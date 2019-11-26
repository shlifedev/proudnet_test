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
    public class GameRoom
    {
        public int identifier = 0;
        public NEntityManager entityManager = new NEntityManager();
        public List<HID> connectedHosts = new List<HID>();
         
        public void JoinClient(HID hostID)
        {
            connectedHosts.Add(hostID);
            foreach(var hid in connectedHosts)
            { 
                server.s2cProxy.NotifyServerMessage(hid, RMI.ReliableSend, $"Player {hostID} Connected.");
            }
        }
        public void LeaveClient(HID hostID)
        {
            connectedHosts.Remove(hostID);
            foreach (var hid in connectedHosts)
            {
                server.s2cProxy.NotifyServerMessage(hid, RMI.ReliableSend, $"Player {hostID} Leave. ");
            }
        }

        public void ShowOnlinePlayer()
        {
            string v = null;
            foreach (var hid in connectedHosts)
            {
                v += hid +" "; 
            }
            v += $"({connectedHosts.Count}onlines)";
            Console.WriteLine("onlines : " + v);
        }
        public int CreateIdentifier()
        {
            identifier++;
            return identifier;
        }
        public GameRoom(GameServer server)
        {
            this.server = server;
        }
        public GameServer server = null;

        public NItemEntity CreateItemEntity(int itemIndex, Vector2 position)
        {
            NItemEntity createEntity = new NItemEntity();
            createEntity.entityIndex = CreateIdentifier();
            createEntity.position = position;
            createEntity.itemIndex = itemIndex;
            for (int i = 0; i < connectedHosts.Count; i++)
            {
                server.s2cProxy.NotifyItemCreate(connectedHosts[i], RMI.ReliableSend, createEntity.entityIndex, createEntity.itemIndex, position);
                Console.WriteLine("send to " + connectedHosts[i]);
            }
            return createEntity;
        }
    }
}
