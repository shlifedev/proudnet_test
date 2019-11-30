using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettention.Proud;
namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            GameTable.Buff.Info.Get(10001);

            Server server = new Server(); 
        }
    }
}
