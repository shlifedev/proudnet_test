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
    public class Player
    {
        public Player(HID hid)
        {
            this.hostID = hid;
            this.inventory = new PlayerInventory();
            this.inventory.player = this;
            this.inventory.inventoryOwnerID = hostID;
            this.buffManager = new NNBuffManager();
        }
        public Struct.NEntity playerEntity = null;
        public HID hostID; 
        public GameRoom room;
        public PlayerInventory inventory;
        public NNBuffManager buffManager;
        public bool IsKiller = false;
        public int job = 0;

        /// <summary>
        /// 플레이어의 아이템사용 요청
        /// </summary> 
        public void OnStubItemUse(HID hid, RMI rmi, int targetEID, int itemEID)
        {
            var target = room.entityManager.entityMap[targetEID];
            var item = room.entityManager.entityMap[itemEID];

            if(item.owner == (int)hid)
            {
                
            }
            else
            {
                Logger.Log(this, "Item Owner Sync Error");
            }
        }
    }

    
}
