using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;

namespace GameServer.ItemExecuteDispatcher
{
    public class ItemPoisonCheck : ItemCommand
    {
        public ItemPoisonCheck(ItemExecuteManager executer) : base(executer)
        {
            this.executeManager = executer;
        }

        public override void Execute(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            if (Executeable(usePlayer, targetEntity, useItem))
            {
                NotifyItemUse(usePlayer, targetEntity, useItem);
            }
        }

        public override bool Executeable(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            if (useItem.RemainUseCount > 0)
            {
                var human = targetEntity as NHumanEntity;
                var dieStatus = human.buffManager.IsDie();
                if (dieStatus)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
