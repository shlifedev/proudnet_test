using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = System.Diagnostics.Debug;
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
            executeMap.Add(EItemType.Kill, new NPCKillItem(this));
        } 
        public void Execute(int playerEID, int targetID, int itemEntityID)
        {
            try
            {
                var player = room.players.GetPlayerByEntityId(playerEID);
                var target = room.entityManager.entityMap[targetID];
                var usedItem = player.inventory.itemMap[itemEntityID];

                var itemdata = usedItem.info;
                executeMap[itemdata.ItemType].Execute(player, target, usedItem);
            }
            catch(System.Exception e)
            {
                Logger.Exception(this, e.Message);
                Logger.Exception(this, $"playerEID :{playerEID}  targetID :{targetID} itemEntityID :{itemEntityID}");
            }
        }
    }
}
    