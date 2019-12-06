using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;
namespace GameServer.Struct
{


    public class NEntityManager
    {
        public NEntityManager(GameRoom room)
        {
            this.room = room;
        }
        public GameRoom room;
        public List<NEntity> entitiList = new List<NEntity>();
        public Dictionary<int, NEntity> entityMap = new Dictionary<int, NEntity>();
        public List<NEntity> playerList = new List<NEntity>();
        public List<NNPCEntity> npcList = new List<NNPCEntity>();

        /// <summary>
        /// 플레이어들의 인벤토리에서 아이템을 찾습니다.
        /// </summary> 
        public Item FindItem(int itemEId)
        {
            for (int i = 0; i < room.players.playerList.Count; i++)
            {
                var exist = room.players.playerList[i].inventory.itemMap.ContainsKey(itemEId);
                if (exist)
                {
                    return room.players.playerList[i].inventory.itemMap[itemEId];
                }
            }
            return null;
        }
        public NEntity FindPlayerEntity(int playerEID)
        {
            if (entityMap.ContainsKey(playerEID))
            {
                return entityMap[playerEID];
            }

            Console.WriteLine("cannot found player eid");
            return null;
        }
        public void AddEntity(int index, NEntity entity)
        {
            if (entityMap.ContainsKey(index) == false)
            {
                entityMap.Add(index, entity);
                entitiList.Add(entity);
            }
        }

        public void RemoveEntity(int index)
        {
            if (entityMap.ContainsKey(index) == true)
            {
                Logger.Log(this, "Entity Remove! =>" + index);
                entitiList.Remove(entityMap[index]);
                entityMap.Remove(index);
            }
        }
        

        public NItemEntity CreateItemEntity(int itemIndex, Vector2 position)
        {
            NItemEntity createEntity = new NItemEntity();
            createEntity.entityIndex = room.CreateIdentifier();
            createEntity.position = position;
            createEntity.itemIndex = itemIndex;

            var itemInfo = GameTable.Item.Info.Get(createEntity.itemIndex);
            createEntity.item.OwnerId = 0;
            createEntity.item.ItemIndex = itemIndex;
            createEntity.item.EntityId = createEntity.entityIndex;
            createEntity.item.MaxUse = itemInfo.MaxUse;
            createEntity.item.RemainUseCount = itemInfo.MaxUse;
            AddEntity(createEntity.entityIndex, createEntity);
            Logger.Log(this, $"Item {itemIndex} Create!");
            return createEntity;
        }
        public NHumanEntity CreatePlayerEntity(Vector2 position, HID ownerID)
        {
            NHumanEntity playerEntity = new NHumanEntity();
            playerEntity.position = position;
            playerEntity.entityIndex = room.CreateIdentifier();
            playerEntity.ownerHostID = (int)ownerID;
            AddEntity(playerEntity.entityIndex, playerEntity);
            playerList.Add(playerEntity);
            Logger.Log(this, $"Player {ownerID} Create!");
            return playerEntity;
        }

       
        public NNPCEntity CreateNPCEntity(int npcIndex, Vector2 position)
        {
            NNPCEntity playerEntity = new NNPCEntity();
            playerEntity.position = position;
            playerEntity.entityIndex = room.CreateIdentifier();
            playerEntity.ownerHostID = (int)HID.HostID_Server;
            AddEntity(playerEntity.entityIndex, playerEntity);
            npcList.Add(playerEntity);
            Logger.Log(this, $"NPC {npcIndex} Create!");
            return playerEntity;
        }
    }

}