
using GameServer.Struct;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;

namespace GameServer.ItemExecuteDispatcher
{
    public abstract class ItemCommand : IItemCommand
    {
        
        public GameRoom Room
        {
            get
            {
                return executeManager.room;
            }
        }
        public ItemCommand(ItemExecuteDispatcher.ItemExecuteManager executer)
        {
            this.executeManager = executer;
        }
        public ItemExecuteDispatcher.ItemExecuteManager executeManager;
        public abstract void Execute(Player usePlayer, NEntity targetEntity, Item useItem);
        public abstract bool Executeable(Player usePlayer, NEntity targetEntity, Item useItem);
        public void NotifyPlayersNPCBuffGive(Item useItem, NEntity targetEntity)
        {
            this.Room.players.playerList.ForEach(x =>
            {
                foreach (var buff in useItem.info.GivenBuff)
                {
                    GameServer.Struct.NBuff nbuff = NBuffManager.CreateBuff(buff, 0);
                    Logger.Log(this, $"NotifyEntityBuffAdd => {x.hostID} (target {targetEntity.entityIndex})");
                    if (targetEntity is NHumanEntity)
                    {
                        var human = targetEntity as NHumanEntity;
                        human.buffManager.AddNBuff(nbuff);
                    }
                    Room.srv.s2cProxy.NotifyEntityBuffAdd(x.hostID, RMI.ReliableSend, targetEntity.entityIndex, nbuff); 
                }
            });
        }
        public void NotifyItemUse(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            Room.srv.s2cProxy.NotifyPlayerItemUse(usePlayer.hostID, RMI.ReliableSend, targetEntity.entityIndex, useItem.EntityId);
        }
    }
}
