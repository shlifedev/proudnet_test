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
    public class Item
    { 
        public int OwnerId;
        public int ItemIndex = 0;
        public int Count = 0;
        public int MaxCount = 0;
        public float Range = 0;
        public int MaxUse = 0;
        public int RemainUseCount = 0;
        public bool StackAble = false;
        public bool DropAble = false;
        public bool TradeAble = false;
    }
}
