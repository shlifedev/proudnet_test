using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HID = Nettention.Proud.HostID;
using RMI = Nettention.Proud.RmiContext;
using Vector3 = UnityEngine.Vector3;
using Struct = GameServer.Struct;
namespace GameServer
{
    public class Player
    {
        public Player(HID hid, GameRoom room)
        {
            this.hostID = hid;
            this.inventory = new PlayerInventory();
            this.inventory.player = this;
            this.inventory.inventoryOwnerID = hostID;
            this.room = room;
            this.room.srv.srv.TickHandler += UpdateBuffTick;
        }
        public Struct.NHumanEntity playerEntity = null;
        public HID hostID;
        public GameRoom room;
        public PlayerInventory inventory;
        public bool IsKiller = false;
        public EJob job = 0;


        /// <summary>
        /// 플레이어의 버프를 업데이트합니다.
        /// </summary>
        /// <param name="e"></param>
        public void UpdateBuffTick(object e)
        {
            if (this.playerEntity != null)
            {
                this.playerEntity.buffManager.RemainTimeManagerBuffs.ForEach(x =>
                {
                    x.remainTime -= 0.1f;
                    if (x.remainTime <= 0) //0이하로 떨어진경우
                    { 
                        if (x.info.Removeable) //삭제할수 있으면 삭제
                        {
                            this.playerEntity.buffManager.RemoveNBuff(x); //삭제가능한경우, 버프지우기
                        }
                        else
                        {
                            this.playerEntity.buffManager.RemoveRemainTimeNBuff(x); //삭제되지 않는 버프의경우(독이라던지..), 남겨놓고 틱리스트에서만 제거.
                        }
                        OnBuffNextBuff(x);
                    }
                });
            }
        }


        public void OnBuffNextBuff(GameServer.Struct.NBuff buff)
        {
            if (buff.info.EndNextBuff.Count > 0)
            {
               
            }
        }


    }


}
