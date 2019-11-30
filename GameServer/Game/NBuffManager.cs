using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Struct;
namespace GameServer
{

    public class NNBuffManager
    {
        public Player owner;
        public System.Action<NBuff> onNBuffAdded; 
        public System.Action<NBuff> onNBuffAdd;
        public System.Action<NBuff> onNBuffRemoved;
        public System.Action<NBuff> onNBuffRemove;

        public List<NBuff> NBuffList = new List<NBuff>();
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

  
        public void AddNBuff(int buffIndex)
        {
            NBuff buff = new NBuff();
            buff.givenTime = owner.room.srv.srv.GetTimeMs();
            //buff.buffType = GameTable.Buff.Info.Get(buffIndex);

           // buff.endTIme = owner.room.srv.srv.GetTimeMs();
            onNBuffAdd?.Invoke(buff);
            if (!IsHasNBuff(buff.buffType))
            {
                this.NBuffList.Add(buff);
            }
            onNBuffAdded?.Invoke(buff);
        }
        public void RemoveNBuff(NBuff NBuff)
        {
            onNBuffRemove?.Invoke(NBuff);
            if (IsHasNBuff(NBuff.buffType))
            {
                this.NBuffList.Remove(NBuff);
            }
            onNBuffRemoved?.Invoke(NBuff);
        }
    }
}
