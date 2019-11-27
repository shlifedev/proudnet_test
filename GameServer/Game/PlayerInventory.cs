using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;

namespace GameServer
{
    public class PlayerInventory
    {

        public HID ownerID;
        public List<Item> itemList = new List<Item>(); 
        public Dictionary<int, Item> itemMap = new Dictionary<int, Item>();

        public void AddItem(int id)
        {

        }

        public void RemoveItem(int id)
        {

        }
    }
}
