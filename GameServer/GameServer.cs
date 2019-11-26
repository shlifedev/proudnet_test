using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettention.Proud;
namespace GameServer
{
    /// <summary>
    /// 테스트서버
    /// </summary>
    public class GameServer
    {        // a P2P group where all clients are in.
        static HostID g_groupHostID = HostID.HostID_None;
        public NetServer srv;
        public GameRoom testRoom = null;
        public Game.Network.C2S.Proxy c2sProxy = new Game.Network.C2S.Proxy();
        public Game.Network.C2S.Stub c2sStub = new Game.Network.C2S.Stub();
        public Game.Network.S2C.Proxy s2cProxy = new Game.Network.S2C.Proxy();
        public Game.Network.S2C.Stub s2cStub = new Game.Network.S2C.Stub();

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
                Console.Write($"Player {lastConnectedHostID.hostID} 접속함. 테스트룸에 추가합니다. ");
                testRoom.JoinClient(lastConnectedHostID.hostID);
            };

            // set a routine for client leave event.
            srv.ClientLeaveHandler = (clientInfo, errorInfo, comment) =>
            {
                Console.Write("Player {0} disconnected.\n", clientInfo.hostID);
                testRoom.LeaveClient(clientInfo.hostID);
            };

        }

        public void ConsoleCommand()
        {
            var v = Console.ReadLine();
            if (v == "CreateItem")
            {
                if (testRoom != null)
                {
                    var createdItem = testRoom.CreateItemEntity(101, new UnityEngine.Vector3(0, 2));
                    Console.WriteLine(createdItem.entityIndex + "Created");
                }
            }
            if (v == "Online")
            {
                if (testRoom != null)
                {
                    testRoom.ShowOnlinePlayer();
                }
            }
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
                testRoom = new GameRoom(this);
            }
            catch (Exception e)
            {
                Console.Write("Server start failed: {0}\n", e.ToString());
                return;
            }

            while (true)
            {
                ConsoleCommand();
            }
        }
    }

}


