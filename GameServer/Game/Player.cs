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
        public Player(HID hid, GameRoom room)
        {
            this.hostID = hid;
            this.inventory = new PlayerInventory();
            this.inventory.player = this;
            this.inventory.inventoryOwnerID = hostID;
            this.buffManager = new NNBuffManager();
            this.room = room; 
        }
        public Struct.NEntity playerEntity = null;
        public HID hostID;
        public GameRoom room;
        public PlayerInventory inventory;
        public NNBuffManager buffManager;
        public bool IsKiller = false;
        public int job = 0;

       
    }


}
