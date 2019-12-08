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
    /// <summary>
    /// 현재 게임 서버룸
    /// </summary>
    public class GameRoom
    {

        public Players players;
        public ItemExecuteDispatcher.ItemExecuteManager itemExecuteManager;
        public GameRoom(GameRoomServer server, Server gameServer)
        {
            this.srv = server;
            this.gSrv = gameServer;
            this.srv.room = this;
            itemExecuteManager = new ItemExecuteDispatcher.ItemExecuteManager(this);
            itemExecuteManager.InitializeExecuteList();
            srv.c2sStub.ReqUseItem += itemExecuteManager.Execute;
            players = new Players();
            players.room = this;
            entityManager = new NEntityManager(this);
            //임시로 스텁처리
            srv.c2sStub.ReqMove += OnReqEnityMove;
            srv.StartServer();
            gameManager = new GameManager(this);
            Logger.Log(this, "GameRoom Setting Succesfully");
            this.CreateNPCEntity(new Vector2(-4, 14.0f));
            this.CreateNPCEntity(new Vector2(6.25f, 12.5f));

            //임시로 처리 
            this.srv.srv.TickHandler += (object e) =>
            {
                if (this.gameRule.currentKillWaitTimer >= 0)
                {
                    Console.WriteLine(this.gameRule.currentKillWaitTimer);
                    this.gameRule.currentKillWaitTimer -= 0.1f;
                }
            };
        }
        public Server gSrv = null;
        public int identifier = 0;
        public NEntityManager entityManager;
        public List<HID> connectedHosts = new List<HID>();
        public GameRoomServer srv = new GameRoomServer();
        public GameManager gameManager;
        public GameRule gameRule = new GameRule();
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
        public bool OnReqEnityMove(HID requester, RMI rmi, int entityId, UnityEngine.Vector3 pos, Vector3 vel)
        {
            var entity = entityManager.entityMap[entityId];

            if (entity != null)
            {
                entity.position = pos;
                foreach (var data in GetOthers(requester))
                {
                    srv.s2cProxy.NotifyEntityMove(data, RMI.ReliableSend, entityId, pos, vel);
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
            players.AddPlayer(playerEntity, hostID, this);
            foreach (var hid in connectedHosts)
            {
                srv.s2cProxy.NotifyServerMessage(hid, RMI.ReliableSend, $"Player {hostID} Connected.");
                srv.s2cProxy.NotifyPlayerCreate(hid, RMI.ReliableSend, playerEntity.ownerHostID, playerEntity.entityIndex, playerEntity.position);

            }
            srv.s2cProxy.NotifyNPCList(hostID, RMI.ReliableSend, new NNPCEntityList()
            {
                count = entityManager.npcList.Count,
                list = entityManager.npcList
            });
            srv.s2cProxy.NotifyJoinPlayer(hostID, RMI.ReliableSend, new NEntityList
            {
                list = entityManager.playerList,
                count = entityManager.playerList.Count
            });
        }
        public void LeaveClient(HID hostID)
        {
            connectedHosts.Remove(hostID);
            var player = players.GetPlayerByEntityId((int)hostID);
            players.RemovePlayer(hostID);
            var entity = entityManager.entitiList.Find(x => x.ownerHostID == (int)hostID);
            if (entity != null)
            {
                entityManager.RemoveEntity(entity.entityIndex);
            }
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
            gameManager.SetRandomJob();
            gameManager.SettingPlayerStartItems();
            //플레이어 랜덤킬러 설정
            gameManager.SetRandomKiller();
            this.srv.srv.TickHandler += UpdateBuffTick;

        }
        public void EndGame()
        {

        }
        public NHumanEntity CreatePlayerEntity(Vector2 position, HID ownerID)
        {
            NHumanEntity playerEntity = entityManager.CreatePlayerEntity(position, ownerID);
            for (int i = 0; i < connectedHosts.Count; i++)
            {
                srv.s2cProxy.NotifyServerMessage(connectedHosts[i], RMI.ReliableSend, $"[Log]Player {ownerID} Created!");
            }
            return playerEntity;
        }
        public NEntity CreateNPCEntity(Vector2 position)
        {
            NEntity playerEntity = entityManager.CreateNPCEntity(CreateIdentifier(), position);
            for (int i = 0; i < connectedHosts.Count; i++)
            {
                srv.s2cProxy.NotifyServerMessage(connectedHosts[i], RMI.ReliableSend, $"[Log]NPC ? Created! (pos:{position})");
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

        #region test_impl
        /// <summary>
        /// 플레이어의 버프를 업데이트합니다.
        /// </summary>
        /// <param name="e"></param>
        public void UpdateBuffTick(object e)
        {

            for (int i = 0; i < entityManager.npcList.Count; i++)
            {
                var npc =entityManager.npcList[i];
                if (npc != null)
                {
                    npc.buffManager.RemainTimeManagerBuffs.ForEach(x =>
                    {
                        x.remainTime -= 0.1f;
                        Console.WriteLine(x.remainTime);
                        if (x.remainTime <= 0) //0이하로 떨어진경우
                        {
                            if (x.info.Removeable) //삭제할수 있으면 삭제
                            {
                                npc.buffManager.RemoveNBuff(x); //삭제가능한경우, 버프지우기
                            }
                            else
                            {
                                npc.buffManager.RemoveRemainTimeNBuff(x); //삭제되지 않는 버프의경우(독이라던지..), 남겨놓고 틱리스트에서만 제거.
                            }
                            OnBuffNextBuff(npc, x);
                        }
                    });
                }
            }
        }

        public void OnBuffNextBuff(NNPCEntity npc, GameServer.Struct.NBuff buff)
        {
            Console.WriteLine(buff.info.Index + "," + buff.info.EndNextBuff.Count);
            if (buff.info.EndNextBuff.Count != 0)
            {
                this.players.playerList.ForEach(x =>
                {
                    foreach (var data in buff.info.EndNextBuff)
                    {
                        Console.WriteLine($"create buff and send {x.hostID} {npc.entityIndex} {buff}");
                        var createbuff = NBuffManager.CreateBuff(data, 0);
                        npc.buffManager.AddNBuff(createbuff);

                        if (createbuff.info.BuffType == EBuffType.Die)
                        {
                            this.gameRule.currentKillWaitTimer = 120.0f;
                        }
                        this.srv.s2cProxy.NotifyEntityBuffAdd(x.hostID, RMI.ReliableSend, npc.entityIndex, createbuff);
                    }
                });

            }
        }
        #endregion
    }
}
