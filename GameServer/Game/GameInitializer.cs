using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;
namespace GameServer
{
    /// <summary> 
    /// 게임 이니셜라이저, 필요한 정보를 로드하는곳.
    /// </summary>
    public class GameInitializer
    {
        public GameInitializer(GameRoom room)
        {
            this.room = room;
            Logger.Log(this, $"GameInitializer Created!"); 
        }
        public GameRoom room;
        public List<Struct.Item> ItemList = new List<Struct.Item>();
       
        public void Init()
        { 
            ItemList.Clear(); 
            GameTable.GameData.StartItemSetting.Load();
            for (int i = 0; i < GameTable.GameData.StartItemSetting.list.Count; i++)
            { 
                for (int j = 0; j < GameTable.GameData.StartItemSetting.list[i].Count; j++)
                { 
                    var idx = GameTable.GameData.StartItemSetting.list[i].Index;
                    var itemInfo = GameTable.Item.Info.Get(idx);
                    Struct.Item item = new Struct.Item();
                    item.EntityId = room.CreateIdentifier();
                    item.ItemIndex = itemInfo.Index;
                    item.MaxUse = itemInfo.MaxUse;
                    item.RemainUseCount = itemInfo.MaxUse;
                    item.OwnerId = 0;
                    this.ItemList.Add(item); 
                }
            }
            Logger.Log(this, "PlayerItemList Initialized!");
        }

        /// <summary>
        /// 플레이어에게 나누어주는 작업.
        /// </summary>
        public void SplitAndGiveToPlayer(Players players)
        {
            for (int i = 0; i < players.playerList.Count; i++)
            {
                var player = players.playerList[i]; 
            }
        }

    }
}
