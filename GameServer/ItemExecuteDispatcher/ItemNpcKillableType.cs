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
                NotifyItemUse(usePlayer, targetEntity, useItem);
                NotifyPlayersNPCBuffGive(useItem, targetEntity);
            }
            else
            {

            }
        }
    }
}

