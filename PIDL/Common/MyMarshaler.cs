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

}