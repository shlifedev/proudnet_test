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
            if (room.entityManager.entityMap.ContainsKey(targetEID))
            {
                var usePlayer = room.players.GetPlayerByEntityId(playerEID); 
                if (usePlayer.inventory.HasItem(itemEID))
                { 
                    var target = room.entityManager.entityMap[targetEID];
                    var item = usePlayer.inventory.itemMap[itemEID];
                    Logger.Log(this, "OnStubItemuseReq... ");
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
                        var buffId = GameTable.Item.Info.Get(item.ItemIndex).GivenBuff;
                        this.room.players.playerList.ForEach(x =>
                        {
                            Logger.Log(this, $"NotifyEntityBuffAdd => {x.hostID} (target {targetEID})");
                            room.srv.s2cProxy.NotifyEntityBuffAdd(x.hostID, RMI.ReliableSend, targetEID, buffId);
                        });
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
