using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Struct = GameServer.Struct;
namespace GameServer
{
    public class Players
    {
        public GameRoom room;
        public List<Player> playerList = new List<Player>();


        /// <summary>
        /// 플레이어의 아이템사용 요청
        /// </summary> 
        public bool OnStubItemUseReq(HID hid, RMI rmi, int playerEID, int targetEID, int itemEID)
        {
            if (room.entityManager.entityMap.ContainsKey(targetEID)) //타겟이 엔티티 리스트에 있는가.
            {
                var usePlayer = room.players.GetPlayerByEntityId(playerEID);  //플레이어를 가져온다. 
                if (usePlayer.inventory.HasItem(itemEID)) //플레이어 인벤토리에 아이템이 존재하나 확인
                {
                    //타겟의 엔티티를 가져온다.
                    var target = room.entityManager.entityMap[targetEID];
                    //아이템을 가져온다.
                    var item = usePlayer.inventory.itemMap[itemEID];
                    if (item.OwnerId == (int)GetPlayerByEntityId(playerEID).hostID)
                    {
                        //사용자의 사용수량 감소처리 및 살인아이템 사용검증 
                        var useItem = usePlayer.inventory.itemList.Find(x => x.EntityId == itemEID);
                        useItem.RemainUseCount -= 1;
                        if (useItem.RemainUseCount <= 0)
                        {
                            Logger.Log(this, "need impl use item remove processing");
                        }
                        Logger.Log(this, $"Player {hid} used item => {useItem.ItemIndex}({itemEID})");
                        //엔피시에게 버프적용
                        if (GameTable.Item.Info.Get(useItem.ItemIndex).IsKillerItem)
                        {
                            this.room.players.playerList.ForEach(x =>
                            {
                                foreach (var buff in GameTable.Item.Info.Get(item.ItemIndex).GivenBuff)
                                {
                                    GameServer.Struct.NBuff nbuff = new Struct.NBuff();
                                    nbuff.endTIme = float.PositiveInfinity;
                                    nbuff.givenTime = room.srv.srv.GetTimeMs();
                                    nbuff.buffType = GameTable.Buff.Info.Get(buff).BuffType;
                                    nbuff.buffIndex = GameTable.Buff.Info.Get(buff).Index;
                                    Logger.Log(this, $"NotifyEntityBuffAdd => {x.hostID} (target {targetEID})");
                                    room.srv.s2cProxy.NotifyEntityBuffAdd(x.hostID, RMI.ReliableSend, targetEID, nbuff);
                                }
                            });
                        }
                        else
                        {
                            //살인 아이템이 아닌경우 개인에게만 적용.
                            room.srv.s2cProxy.NotifyPlayerItemUse(usePlayer.hostID, RMI.ReliableSend, targetEID, itemEID);
                        }
                        return true;
                    }
                    else
                    {
                        Logger.Log(this, "Item Owner Sync Error");
                        return false;
                    }
                }
                else
                {
                }
            }
            else
            {

            }
            return true;
        }


        public void AddPlayer(Struct.NEntity playerEntity, HID hid, GameRoom registGameRoom)
        {
            Player player = new Player(hid, registGameRoom);
            player.playerEntity = playerEntity;
            Logger.Log(this, $"Player {player.hostID} Added  ");
            this.playerList.Add(player);
        }

        public void RemovePlayer(HID hid)
        {
            playerList.Remove(playerList.Find(x => x.hostID == hid));
        }


        public Player GetPlayerByEntity(Struct.NEntity entity)
        {
            var entityId = entity.entityIndex;
            return GetPlayerByEntityId(entityId);
        }
        public Player GetPlayerByEntityId(int id)
        {
            foreach (var data in playerList)
            {
                if (data.playerEntity.entityIndex == id)
                {
                    return data;
                }
            }
            return null;
        }

        public Player GetPlayerByEntityId(HID id)
        {
            return GetPlayerByEntityId((int)id);
        }

        public Player GetPlayerByHostId(int id)
        {
            foreach (var data in playerList)
            {
                if ((int)data.hostID == id)
                {
                    return data;
                }
            }
            return null;
        }
    }
}
