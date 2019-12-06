using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettention.Proud;
namespace GameTestVirtualClient
{
    public class Vars
    {
        public static System.Guid m_Version = new System.Guid("{ 0x3ae33249, 0xecc6, 0x4980, { 0xbc, 0x5d, 0x7b, 0xa, 0x99, 0x9c, 0x7, 0x39 } }");
        public static int m_serverPort = 33334;
    }

    class Program
    {
      
        public static int count = 5;
        static void Main(string[] args)
        {
            List<NetClient> clients = new List<NetClient>();
            for(int i = 0; i < count; i++)
            {
                NetClient client = new NetClient();
                Nettention.Proud.NetConnectionParam param = new Nettention.Proud.NetConnectionParam();
                param.protocolVersion.Set(Vars.m_Version);
                param.serverPort = (ushort)Vars.m_serverPort;
                param.serverIP = "121.140.182.71";
                client.Connect(param);
                client.JoinServerCompleteHandler = (errironfo, rply) => { 
                };
                clients.Add(client);
                System.Threading.Thread.Sleep(250);
            } 

            while(true)
            {
                System.Threading.Thread.Sleep(1000);
                foreach (var c in clients)
                {
                    c.FrameMove();
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
