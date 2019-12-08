using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameRule
    { 
        public float default_killWaitTimer = 120;
        public float currentKillWaitTimer = 0.0f;
        public int   autoGameStartPlayer = 3;
        public bool  autoGameStart = false; 
        
    }
}
