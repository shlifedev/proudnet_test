using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;

namespace GameServer.ItemExecuteDispatcher
{
    public class NPCKillItem : ItemCommand
    {
        public NPCKillItem(ItemExecuteManager executer) : base(executer)
        {
            this.executeManager = executer;
        }

        public override void Execute(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            if(usePlayer.IsKiller)
            {
                if(useItem.info.IsKillerItem)
                {
                    if(useItem.info.ItemTargetType == EItemTargetType.NPCTarget)
                    {
                         
                    }
                }
            }
        }
    }
}
