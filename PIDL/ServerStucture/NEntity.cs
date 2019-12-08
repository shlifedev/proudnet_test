using System.Collections.Generic;
using System.Collections;
namespace GameServer.Struct
{

    public class NEntityList
    {
        public int count = 0;
        public List<NEntity> list = new List<NEntity>();
    }

    public class NNPCEntityList
    {
        public int count = 0;
        public List<NNPCEntity> list = new List<NNPCEntity>();
    }
    public class NEntity
    {
        public int ownerHostID = -1;
        public int entityIndex;
        public UnityEngine.Vector3 position;
    }


    public class NHumanEntity : NEntity
    {
        public NBuffManager buffManager = new NBuffManager();
    }
    public class NPlayerEntity : NHumanEntity
    { 
        
    }

    public class NNPCEntity : NHumanEntity
    { 
        public bool die = false;
    }
    public class NItemEntity : NEntity
    {
        public int itemIndex;
        public Item item;
    }
}