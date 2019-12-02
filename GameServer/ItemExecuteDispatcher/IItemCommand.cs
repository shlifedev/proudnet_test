using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;
namespace GameServer
{
    public interface IItemCommand
    {
        bool Executeable(Player usePlayer, NEntity targetEntity, Item useItem);
        void Execute(Player usePlayer, NEntity targetEntity, Item useItem);
    }
}
