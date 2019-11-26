using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettention.Proud;
namespace GameServer
{
    class GameServer
    {        // a P2P group where all clients are in.
        static HostID g_groupHostID = HostID.HostID_None;
        NetServer srv;
        Game.Network.C2S.Proxy c2sProxy = new Game.Network.C2S.Proxy();
        Game.Network.C2S.Stub c2sStub = new Game.Network.C2S.Stub();
        Game.Network.S2C.Proxy s2cProxy = new Game.Network.S2C.Proxy();
        Game.Network.S2C.Stub s2cStub = new Game.Network.S2C.Stub();
        private void InitStub()
        {
            this.srv.AttachProxy(c2sProxy);
            this.srv.AttachStub(c2sStub);
            this.srv.AttachProxy(s2cProxy);
            this.srv.AttachStub(s2cStub); 
        } 

        private void ServerCallbackHandle()
        {
            srv.ClientJoinHandler = (lastConnectedHostID) =>
            {
                Console.Write("Client {0} connected.\n", lastConnectedHostID.hostID); ;
            };

            // set a routine for client leave event.
            srv.ClientLeaveHandler = (clientInfo, errorInfo, comment) =>
            {
                Console.Write("Client {0} disconnected.\n", clientInfo.hostID);
            };

        }
        public GameServer()
        {
            srv = new NetServer();
            ServerCallbackHandle();
            StartServerParameter ssp = new StartServerParameter();
            ssp.tcpPorts.Add(Vars.m_serverPort);
            ssp.protocolVersion = new Nettention.Proud.Guid(Vars.m_Version);
            InitStub();

            //서버시작
            try
            {
                Console.Write("Server start");
                srv.Start(ssp);
            }
            catch (Exception e)
            {
                Console.Write("Server start failed: {0}\n", e.ToString());
                return;
            }
       
            while (true)
            { 
                //Server Main Loop (mainThread)
            }
        }
    }

}


