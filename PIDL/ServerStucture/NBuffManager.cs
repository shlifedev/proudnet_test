using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;
namespace GameServer
{

    public class NBuffManager
    {
        public static NBuff CreateBuff(int buffIndex, float createTime)
        {
            return new NBuff()
            {
                buffIndex = buffIndex,
                buffType = GameTable.Buff.Info.Get(buffIndex).BuffType,
                givenTime = createTime,
                endTIme = -1,
                remainTime = GameTable.Buff.Info.Get(buffIndex).BuffTime
            };
        }
        public System.Action<NBuff> onNBuffAdded;
        public System.Action<NBuff> onNBuffAdd;
        public System.Action<NBuff> onNBuffRemoved;
        public System.Action<NBuff> onNBuffRemove;
        public List<NBuff> NBuffList = new List<NBuff>();
        public List<NBuff> RemainTimeManagerBuffs = new List<NBuff>();

        public bool IsHasNBuff(EBuffType buffType)
        {
            var NBuff = NBuffList.Find(x => x.buffType == buffType);
            return NBuff != null;
        }

        public bool IsDie()
        {
            var NBuff = NBuffList.Find(x => x.buffType == EBuffType.Die);
            return NBuff != null;
        }

        public void AddNBuff(NBuff buff)
        {
            onNBuffAdd?.Invoke(buff);
            if (!IsHasNBuff(buff.buffType))
            {
                this.NBuffList.Add(buff);
                if (buff.remainTime != -1)
                {
                    RemainTimeManagerBuffs.Add(buff);
                }
            }
            onNBuffAdded?.Invoke(buff);
        }
        public void RemoveNBuff(NBuff NBuff)
        {
            onNBuffRemove?.Invoke(NBuff);
            if (IsHasNBuff(NBuff.buffType))
            {
                this.NBuffList.Remove(NBuff);
                if (RemainTimeManagerBuffs.Contains(NBuff))
                    this.RemainTimeManagerBuffs.Remove(NBuff);
            }
            onNBuffRemoved?.Invoke(NBuff);
        }

        public void RemoveRemainTimeNBuff(NBuff NBuff)
        {
            onNBuffRemove?.Invoke(NBuff);
            if (IsHasNBuff(NBuff.buffType))
            {
                if (RemainTimeManagerBuffs.Contains(NBuff))
                    this.RemainTimeManagerBuffs.Remove(NBuff);
            }
            onNBuffRemoved?.Invoke(NBuff);
        }
    }
}
