using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    /// <summary>
    /// 게임 매니저, 게임의 플로우를 제어하는곳.
    /// </summary>
    public class GameManager
    {
        private GameInitializer gameInitializer;
        public GameManager(GameRoom room)
        {
            Logger.Log(this, "GameManager Created!");
            gameInitializer = new GameInitializer(room);
            this.room = room;
        }
        public GameRoom room;


        
        public void SetRandomKiller()
        {
            System.Random rand = new Random();
            var picked = rand.Next(0, room.players.playerList.Count);
            var player = room.players.playerList[picked];
            player.IsKiller = true;
            Logger.Log(this, $"Player{player.hostID} Is a Killer Now!");

        }
        public void SettingPlayerStartItems()
        {
            Logger.Log(this, "SettingPlayerStartItems... ");
            gameInitializer.Init(); 
            var itemList = gameInitializer.ItemList;
            Logger.Log(this, "Loaded Item List :" + itemList.Count); 
            var killerItems = itemList.FindAll(x => GameTable.Item.Info.Get(x.ItemIndex).IsKillerItem == true);
            var citizenItems = itemList.FindAll(x => GameTable.Item.Info.Get(x.ItemIndex).IsKillerItem == false);
            Logger.Log(this, "Loaded Killer Item Count:" + killerItems.Count);
            Logger.Log(this, "Loaded CitizenItems Item Count:" + citizenItems.Count);
            System.Random rand = new Random();
            Logger.Log(this, "Online Players Count:" + room.players.playerList.Count);
            for (int i = 0; i < room.players.playerList.Count; i++)
            {
                var player = room.players.playerList[i];
                string pickLog = null;
                for(int j = 0; j < 3; j++)
                {
                    var r = rand.Next(0, killerItems.Count);
                    var pick = killerItems[r];
                    pickLog += GameTable.Item.Translate_Name.Get(pick.ItemIndex).KR +" ";
                    killerItems.Remove(pick);
                    GiveItem(player, pick);
                }

                var dobogi = (citizenItems.Find(x => x.ItemIndex == 130));
                if(dobogi != null)
                {
                    GiveItem(player, dobogi);
                }
                Logger.Log(this, $"Give Item Player{player.hostID} => " + pickLog);
            }    
        }
        public void GiveItem(Player player, GameServer.Struct.Item item)
        {
            if (item.OwnerId == 0)
            {
                player.inventory.AddItem(item); 
            }
        }
    }
}
