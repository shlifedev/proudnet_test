using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;
using UnityEngine;
using Nettention.Proud;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;

namespace GameServer
{

    public class GameRoomServer
    {

        public GameRoom room;
        public NetServer srv = new NetServer();
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
                Logger.Log(this, $"Player {lastConnectedHostID.hostID} 접속함. 테스트룸에 추가합니다. ");
                room.JoinClient(lastConnectedHostID.hostID);
            };

            // set a routine for client leave event.
            srv.ClientLeaveHandler = (clientInfo, errorInfo, comment) =>
            {
                Logger.Log(this, $"Player {clientInfo.hostID} disconnect");
                room.LeaveClient(clientInfo.hostID);
            };

        }
        public void ConsoleCommand(string cmd)
        {
            var v = cmd;
            if (v == "CreateItem")
            {
                if (room != null)
                {
                    var createdItem = room.CreateItemEntity(101, new UnityEngine.Vector3(0, 2));
                    Console.WriteLine(createdItem.entityIndex + "Created");
                }
            }
            if (v == "Online")
            {
                if (room != null)
                {
                    room.ShowOnlinePlayer();
                }
            }
            if (v == "Start")
            {
                room.StartGame();
            }

            if (v == "EntityManager.Debug")
            {
                Console.WriteLine("entity::");
                for (int i = 0; i < room.entityManager.entitiList.Count; i++)
                {
                    Console.WriteLine(room.entityManager.entitiList[i].entityIndex + "registed!");
                }
                Console.WriteLine("playerlist::");
                for (int i = 0; i < room.entityManager.playerList.Count; i++)
                {
                    Console.WriteLine(room.entityManager.playerList[i].ownerHostID + "registed!");
                }
            }

            if(v.Contains("NPC.BuffList"))
            {
                Console.Write("NPC EID 번호 입력:");
                var npcIndex = Console.ReadLine();
                if(room.entityManager.entityMap.ContainsKey(int.Parse(npcIndex)))
                {
                    var data = room.entityManager.entityMap[int.Parse(npcIndex)];
                    Console.WriteLine($"NPC {data.entityIndex} BuffList");
                    foreach(var buff in (data as NNPCEntity).buffManager.NBuffList)
                    {
                        Console.WriteLine(GameTable.Buff.Translate_Name.Get(buff.buffIndex).KR);
                    }
                }
            }
        }
        public void StartServer()
        {
            srv = new NetServer();
            ServerCallbackHandle();
            InitStub();
            StartServerParameter ssp = new StartServerParameter();
            ssp.tcpPorts.Add(Vars.m_serverPort);
            ssp.protocolVersion = new Nettention.Proud.Guid(Vars.m_Version);


            //서버시작
            try
            {

                Logger.Log(this, $"GameRoom Server Start | Port {Vars.m_serverPort} \n");
                srv.Start(ssp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Server start failed: {0}\n", e.ToString());
                return;
            }
        }
    }
}