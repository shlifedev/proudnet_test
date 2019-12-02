using System.Collections.Generic;
using System.Collections;
namespace GameServer.Struct
{
    [System.Serializable]
    public class Item
    {
        public int EntityId;
        public int OwnerId;
        public int ItemIndex = 0;
        public int MaxUse = 0;
        public int RemainUseCount = 0; 
        public GameTable.Item.Info info
        {
            get
            {
                return GameTable.Item.Info.Get(this.ItemIndex);
            }
        } 
    }

}
