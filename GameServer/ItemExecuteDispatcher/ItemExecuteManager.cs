using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = System.Diagnostics.Debug;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;
namespace GameServer.ItemExecuteDispatcher
{
    public class ItemExecuteManager
    {
        public GameRoom room;
        public Dictionary<EItemType, IItemCommand> executeMap = new Dictionary <EItemType, IItemCommand>();
        public ItemExecuteManager(GameRoom room)
        {
            this.room = room;
        }

        public void InitializeExecuteList()
        {
            executeMap.Add(EItemType.Kill, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.CorpseStatus, new ItemCorpseStatus(this));
        }

        public bool Assert(int playerEID, int targetID, int itemEntityID)
        {
            if (room.players.GetPlayerByEntityId(playerEID) == null)
            {
                Console.WriteLine("player eid is null" + playerEID);
            }
            if (room.entityManager.entityMap.ContainsKey(targetID) == false)
            {
                Console.WriteLine("targetID eid is null" + targetID);
            }
            if (room.players.GetPlayerByEntityId(playerEID).inventory.itemMap.ContainsKey(itemEntityID) == false)
            {
                Console.WriteLine("player inv item eid is null" + itemEntityID);
            }
            return true;
        }
        public bool Execute(HID id, RMI rmi, int playerEID, int targetID, int itemEntityID)
        {
            try
            {
                Assert(playerEID, targetID, itemEntityID);
                var player = room.players.GetPlayerByEntityId(playerEID);
                var target = room.entityManager.entityMap[targetID];
                var usedItem = player.inventory.itemMap[itemEntityID];
                var itemdata = usedItem.info;
                executeMap[itemdata.ItemType].Execute(player, target, usedItem);
                return true;
            }
            catch (System.Exception e)
            {
                Logger.Exception(this, e.Message);
                Logger.Exception(this, $"playerEID :{playerEID}  targetID :{targetID} itemEntityID :{itemEntityID}");
                return false;
            }

            return true;
        }
    }
}
