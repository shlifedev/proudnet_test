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
        public float givenTime;
        public float endTIme;

       
    }

}
