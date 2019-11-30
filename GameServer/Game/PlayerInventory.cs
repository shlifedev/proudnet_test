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
    public class PlayerInventory
    {
        public Player player;
        public HID inventoryOwnerID;
        public List<Struct.Item> itemList = new List<Struct.Item>(); 
        public Dictionary<int, Struct.Item> itemMap = new Dictionary<int, Struct.Item>();

        public void AddItem(Struct.Item item)
        {
            item.OwnerId = (int)inventoryOwnerID;
            itemList.Add(item);
            itemMap.Add(item.EntityId, item); 
             player.room.srv.s2cProxy.NotifyInventoryItemAdd(inventoryOwnerID, RMI.ReliableSend, item);
        }

        public bool HasItem(int eid)
        {
            return (itemList.Find(x => eid == x.EntityId)) != null;
        }
        public void RemoveItemByEntityId(int entityId)
        {
            var item = itemMap[entityId];
            itemMap.Remove(entityId);
            itemList.Remove(item);
        }
    }
}
