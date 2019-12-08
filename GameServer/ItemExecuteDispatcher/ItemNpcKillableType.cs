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

        /// <summary>
        /// 독이라던지.. 기타등등으로 죽을 예정인 엔피시가 있는지 확인한다.
        /// </summary>
        /// <returns></returns>
        public bool CheckKillSubscribeNPCExist(Player usePlayer)
        {
            bool killSubscribed = false;
            for (int i = 0; i < Room.entityManager.npcList.Count; i++)
            {
                var npc = Room.entityManager.npcList[i];

                npc.buffManager.RemainTimeManagerBuffs.ForEach(x =>
                {
                    x.info.EndNextBuff.ForEach(eb =>
                    {
                        if (GameTable.Buff.Info.Get(eb).BuffType == EBuffType.Die)
                        { 
                            Room.srv.s2cProxy.NotifyServerMessage(usePlayer.hostID, RMI.ReliableSend, "-현재 이 녀석을 죽이기엔 무리가 있는 것 같다.");
                            killSubscribed = true; 
                        }
                    });
                });
            }
            return killSubscribed;
        }

        public bool IsWaitKillTime(Player usePlayer)
        {
            var b =  Room.gameRule.currentKillWaitTimer > 0;
            if(b)
            {
                Room.srv.s2cProxy.NotifyServerMessage(usePlayer.hostID, RMI.ReliableSend, "-너무 빨리 죽이기엔 조금 무리가 있어. 지금은 조금 기다리는게 맞다.");
            }
            return b;
        }
        public override bool Executeable(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            
            if (usePlayer.IsKiller && IsWaitKillTime(usePlayer) == false) 
            {
                if (useItem.info.IsKillerItem && !CheckKillSubscribeNPCExist(usePlayer))
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
                NotifyItemUse(usePlayer, targetEntity, useItem);
                NotifyPlayersNPCBuffGive(useItem, targetEntity);
            }
            else
            {

            }
        }
    }
}

