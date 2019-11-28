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
                Logger.Log(this,$"Player {lastConnectedHostID.hostID} 접속함. 테스트룸에 추가합니다. ");
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
            if( v == "Start")
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
                    Console.WriteLine(room.entityManager.playerList[i].owner + "registed!");
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
    public class GameRoom
    {

        public Players players;
        public GameRoom(GameRoomServer server, Server gameServer)
        {
            this.srv = server;
            this.gSrv = gameServer;
            this.srv.room = this; 
            players = new Players(); 
            players.room = this; 
            entityManager = new NEntityManager(this);
            //임시로 스텁처리
            srv.c2sStub.ReqMove += OnReqEnityMove;
            srv.StartServer();
            gameManager = new GameManager(this); 
            Logger.Log(this, "GameRoom Setting Succesfully");
        }
        public Server gSrv = null;

        public int identifier = 0;
        public NEntityManager entityManager;
        public List<HID> connectedHosts = new List<HID>();
        public GameRoomServer srv = new GameRoomServer();
        public GameManager gameManager;
        public HID[] GetOthers(HID ignore)
        {
            int cur = 0;
            HID[] hids = new HID[connectedHosts.Count - 1];
            for (int i = 0; i < connectedHosts.Count; i++)
            {
                if (connectedHosts[i] != ignore)
                {
                    hids[cur] = connectedHosts[i];
                    cur++;
                }
            }
            return hids;
        }
        public bool OnReqEnityMove(HID requester, RMI rmi, int entityId, UnityEngine.Vector3 pos)
        { 
            var entity = entityManager.entityMap[entityId];

            if (entity != null)
            {
                entity.position = pos; 
                foreach (var data in GetOthers(requester))
                { 
                    srv.s2cProxy.NotifyEntityMove(data, RMI.ReliableSend, entityId, pos);
                }
            }
            else
            {
                Logger.Error(this, $"entity {entityId} not found..");
            }
            return true;
        }
        public void JoinClient(HID hostID)
        {

            connectedHosts.Add(hostID);
            var playerEntity = CreatePlayerEntity(new Vector2(0, 1), hostID);
            players.AddPlayer(playerEntity, hostID,  this);
            foreach (var hid in connectedHosts)
            { 
                srv.s2cProxy.NotifyServerMessage(hid, RMI.ReliableSend, $"Player {hostID} Connected.");
                srv.s2cProxy.NotifyPlayerCreate(hid, RMI.ReliableSend, playerEntity.owner, playerEntity.entityIndex, playerEntity.position);
                
            }

            var entList = new NEntityList();
            entList.list = entityManager.playerList;
            entList.count = entityManager.playerList.Count;
            srv.s2cProxy.NotifyJoinPlayer(hostID, RMI.ReliableSend, entList);
        }
        public void LeaveClient(HID hostID)
        { 
            connectedHosts.Remove(hostID);
            players.RemovePlayer(hostID);
            foreach (var hid in connectedHosts)
            {
                srv.s2cProxy.NotifyServerMessage(hid, RMI.ReliableSend, $"Player {hostID} Leave. ");
            }
        }
        public void ShowOnlinePlayer()
        {
            string v = null;
            foreach (var hid in connectedHosts)
            {
                v += hid + " ";
            }
            v += $"({connectedHosts.Count}onlines)";
            Console.WriteLine("onlines : " + v);
        }
        public int CreateIdentifier()
        {
            identifier++;
            return identifier;
        }

        public void StartGame()
        {
            //플레이어 아이템설정
            gameManager.SettingPlayerStartItems();
            //플레이어 랜덤킬러 설정
            gameManager.SetRandomKiller();
        }
        public void EndGame()
        {

        }
        public NEntity CreatePlayerEntity(Vector2 position, HID ownerID)
        {
            NEntity playerEntity = entityManager.CreatePlayerEntity(position, ownerID);
            for (int i = 0; i < connectedHosts.Count; i++)
            {
                srv.s2cProxy.NotifyServerMessage(connectedHosts[i], RMI.ReliableSend, $"[Log]Player {ownerID} Created!");
            }
            return playerEntity;
        }
        public NItemEntity CreateItemEntity(int itemIndex, Vector2 position)
        {
            NItemEntity createEntity = entityManager.CreateItemEntity(itemIndex, position);
            for (int i = 0; i < connectedHosts.Count; i++)
            {
                srv.s2cProxy.NotifyItemCreate(connectedHosts[i], RMI.ReliableSend, createEntity.entityIndex, createEntity.itemIndex, position);
                Console.WriteLine("send to " + connectedHosts[i]);
            }
            return createEntity;
        }
    }
}
