using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Struct
{
    [System.Serializable]
    public class NBuff
    {
        public int buffIndex; 
        public EBuffType buffType;
        public float remainTime;
        public float givenTime;
        public float endTIme;

        public GameTable.Buff.Info info
        {
            get
            {
                return GameTable.Buff.Info.Get(buffIndex);
            }
        }
        public GameTable.Buff.Translate_Name tranlateName
        {
            get
            {
                return GameTable.Buff.Translate_Name.Get(buffIndex);
            }
        }
    }

}
