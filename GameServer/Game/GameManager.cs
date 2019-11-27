using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
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
