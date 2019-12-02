using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettention.Proud;
namespace GameServer
{
    /// <summary>
    /// 테스트서버
    /// </summary>
    public class Server
    {
        public Server()
        {
            GameRoom room = new GameRoom(new GameRoomServer(), this); 
            while (true)
            {
                var data = Console.ReadLine();
                room.srv.ConsoleCommand(data);
            }
        }
    }

}


