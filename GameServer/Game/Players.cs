using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Struct = GameServer.Struct;
namespace GameServer
{
    public class Players
    {
        public GameRoom room;
        public List<Player> playerList = new List<Player>(); 

        public void AddPlayer(Struct.NHumanEntity playerEntity, HID hid, GameRoom registGameRoom)
        {
            Player player = new Player(hid, registGameRoom);
            player.playerEntity = playerEntity;
            Logger.Log(this, $"Player {player.hostID} Added  ");
            this.playerList.Add(player);
        }

        public void RemovePlayer(HID hid)
        {
            playerList.Remove(playerList.Find(x => x.hostID == hid));
        }


        public Player GetPlayerByEntity(Struct.NHumanEntity entity)
        {
            var entityId = entity.entityIndex;
            return GetPlayerByEntityId(entityId);
        }
        public Player GetPlayerByEntityId(int id)
        {
            foreach (var data in playerList)
            {
                if (data.playerEntity.entityIndex == id)
                {
                    return data;
                }
            }
            return null;
        }

        public Player GetPlayerByEntityId(HID id)
        {
            return GetPlayerByEntityId((int)id);
        }

        public Player GetPlayerByHostId(int id)
        {
            foreach (var data in playerList)
            {
                if ((int)data.hostID == id)
                {
                    return data;
                }
            }
            return null;
        }
    }
}
