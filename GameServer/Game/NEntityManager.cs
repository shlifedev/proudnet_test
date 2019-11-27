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
        public Dictionary<int, NItemEntity> itemEntityMap = new Dictionary<int, NItemEntity>();
        public List<NEntity> playerList = new List<NEntity>();
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
            AddEntity(createEntity.entityIndex, createEntity);
         
            return createEntity;
        }
        public NEntity CreatePlayerEntity(Vector2 position, HID ownerID)
        {
            NEntity playerEntity = new NEntity();
            playerEntity.position = position;
            playerEntity.entityIndex = room.CreateIdentifier();
            playerEntity.owner = (int)ownerID;
            AddEntity(playerEntity.entityIndex, playerEntity);
            playerList.Add(playerEntity);
            return playerEntity;
        }
    }

}