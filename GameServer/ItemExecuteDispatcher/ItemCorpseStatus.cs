using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;

namespace GameServer.ItemExecuteDispatcher
{
    class ItemCorpseStatus : ItemCommand
    {
        public override void Execute(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            throw new NotImplementedException();
        }

        public override bool Executeable(Player usePlayer, NEntity targetEntity, Item useItem)
        {
            throw new NotImplementedException();
        }
    }
}
