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
            //일반적인 살인아이템
            executeMap.Add(EItemType.Knife, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Awl, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Hammer, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.BaseballBat, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.ElectricSaw, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Revolver, new ItemNpcKillableType(this)); 
            executeMap.Add(EItemType.Cyanide, new ItemNpcKillableType(this)); 
            executeMap.Add(EItemType.SkinBelt, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Formalin, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Pencil, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Knuckles, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.PosionNeedle, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.MetalThread, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.SkinGlove, new ItemNpcKillableType(this));
            executeMap.Add(EItemType.Pen, new ItemNpcKillableType(this));


            //시체수거
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

                if (executeMap.ContainsKey(itemdata.ItemType))
                {
                    executeMap[itemdata.ItemType].Execute(player, target, usedItem);
                }
                else
                { 
                    room.srv.s2cProxy.NotifyServerMessage(player.hostID, RMI.ReliableSend, $"sorry item ({usedItem.ItemIndex}) no regist server {itemdata.ItemType}");
                }
                return true;
            }
            catch (System.Exception e)
            {
                Logger.Exception(this, e.Message);
                Logger.Exception(this, $"playerEID :{playerEID}  targetID :{targetID} itemEntityID :{itemEntityID}");
        
                return false;
            } 
        }
    }
}
