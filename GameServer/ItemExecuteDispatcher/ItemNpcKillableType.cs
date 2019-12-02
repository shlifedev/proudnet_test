using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;

namespace GameServer.ItemExecuteDispatcher
{
    public class ItemNpcKillableType : ItemCommand
    {
        public ItemNpcKillableType(ItemExecuteManager executer) : base(executer)
        {
            this.executeManager = executer;
        }
        public override bool Executeable(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            if (usePlayer.IsKiller)
            {
                if (useItem.info.IsKillerItem)
                {
                    if (useItem.info.ItemTargetType == EItemTargetType.NPCTarget)
                    {
                        return true;
                    }
                } 
            }
            return false;
        }
        public override void Execute(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            if (Executeable(usePlayer, targetEntity, useItem))
            {
                useItem.RemainUseCount -= 1;
                if (useItem.RemainUseCount <= 0)
                {
                    Logger.Log(this, "need impl use item remove processing");
                }

                //엔피시에게 버프적용
                if (GameTable.Item.Info.Get(useItem.ItemIndex).IsKillerItem)
                {
                    Room.srv.s2cProxy.NotifyPlayerItemUse(usePlayer.hostID, RMI.ReliableSend, targetEntity.entityIndex, useItem.ItemIndex); 
                    this.executeManager.room.players.playerList.ForEach(x =>
                    {
                        foreach (var buff in useItem.info.GivenBuff)
                        {
                            GameServer.Struct.NBuff nbuff = NNBuffManager.CreateBuff(buff, executeManager.room); 
                            Logger.Log(this, $"NotifyEntityBuffAdd => {x.hostID} (target {targetEntity.entityIndex})");
                            Room.srv.s2cProxy.NotifyEntityBuffAdd(x.hostID, RMI.ReliableSend, targetEntity.entityIndex, nbuff);
                        }
                    }); 
                }
            }
            else
            {
                
            }
        }
    }
}

