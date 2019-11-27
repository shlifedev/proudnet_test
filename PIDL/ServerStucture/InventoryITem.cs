using System.Collections.Generic;
using System.Collections;
namespace GameServer.Struct
{

    public class Item
    {
        public int EntityId;
        public int OwnerId;
        public int ItemIndex = 0;         
        public int MaxUse = 0;
        public int RemainUseCount = 0; 

    }

}