using System.Collections.Generic;
using System.Collections;
namespace GameServer.Struct
{

    public class NEntityList
    { 
        public int count = 0;
        public List<NEntity> list = new List<NEntity>();
    }
    public class NEntity
    {
        public int owner = -1;
        public int entityIndex;
        public UnityEngine.Vector3 position;
    }

    public class NPlayerEntity : NEntity
    { 

    }

    public class NItemEntity : NEntity
    {
        public int itemIndex;
        public Item item;
    }
}