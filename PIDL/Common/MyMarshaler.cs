using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MyMarshaler : Nettention.Proud.Marshaler
{
    public static void Write(Nettention.Proud.Message msg, UnityEngine.Vector3 b)
    {
        msg.Write(b.x);
        msg.Write(b.y);
        msg.Write(b.z);
    }

    public static void Read(Nettention.Proud.Message msg, out UnityEngine.Vector3 b)
    {
        b = new UnityEngine.Vector3();
        msg.Read(out b.x);
        msg.Read(out b.y);
        msg.Read(out b.z);

    }

    public static void Write(Nettention.Proud.Message msg, GameServer.Struct.NEntity entity)
    {
        msg.Write(entity.ownerHostID);
        msg.Write(entity.entityIndex);
        msg.Write(entity.position.x);
        msg.Write(entity.position.y);
        msg.Write(entity.position.z);
    }
    public static void Read(Nettention.Proud.Message msg, out GameServer.Struct.NEntity entity)
    {
        entity = new GameServer.Struct.NEntity();
        msg.Read(out entity.ownerHostID);
        msg.Read(out entity.entityIndex);
        msg.Read(out entity.position.x);
        msg.Read(out entity.position.y);
        msg.Read(out entity.position.z);
    }

    public static void Write(Nettention.Proud.Message msg, GameServer.Struct.Item item)
    {
        msg.Write(item.EntityId);
        msg.Write(item.OwnerId);
        msg.Write(item.ItemIndex);
        msg.Write(item.MaxUse);
        msg.Write(item.RemainUseCount);
    }
    public static void Read(Nettention.Proud.Message msg, out GameServer.Struct.Item item)
    {
        item = new GameServer.Struct.Item();
        msg.Read(out item.EntityId);
        msg.Read(out item.OwnerId);
        msg.Read(out item.ItemIndex);
        msg.Read(out item.MaxUse);
        msg.Read(out item.RemainUseCount);
    }
    public static void Write(Nettention.Proud.Message msg, GameServer.Struct.NEntityList entity)
    {
        msg.Write(entity.count);
        for (int i = 0; i < entity.list.Count; i++)
        {
            Write(msg, entity.list[i]);
        }
    }
    public static void Read(Nettention.Proud.Message msg, out GameServer.Struct.NEntityList entity)
    {
        entity = new GameServer.Struct.NEntityList();
        msg.Read(out entity.count);
        for (int i = 0; i < entity.count; i++)
        {
            var ent = new GameServer.Struct.NEntity();
            Read(msg, out ent);
            entity.list.Add(ent);
        }
    }
 
    public static void Write(Nettention.Proud.Message msg, GameServer.Struct.NBuff buff)
    {
        msg.Write(buff.buffIndex);
        msg.Write((int)buff.buffType);
        msg.Write(buff.givenTime);
        msg.Write(buff.endTIme); 
    }
    public static void Read(Nettention.Proud.Message msg, out GameServer.Struct.NBuff buff)
    {
        buff = new GameServer.Struct.NBuff();
        int ebufftype = 0 ;
        msg.Read(out buff.buffIndex);
        msg.Read(out ebufftype);
        buff.buffType = (EBuffType)ebufftype;
        msg.Read(out buff.givenTime);
        msg.Read(out buff.endTIme);
    }

}