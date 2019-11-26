using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Network
{
    public class NEntity
    {
        public int entityIndex;
        public UnityEngine.Vector3 position;
    }

    public class NItemEntity : NEntity
    {

    }

    public class NEntityManager
    {
        public List<NEntity> entitiList = new List<NEntity>();
        public Dictionary<int, NEntity> entityMap = new Dictionary<int, NEntity>();
        public Dictionary<int, NItemEntity> itemEntityMap = new Dictionary<int, NItemEntity>();
        public void AddEntity(int index, NEntity entity)
        {
            if (entityMap.ContainsKey(index) == false)
            {
                entityMap.Add(index, entity);
                entitiList.Add(entity);
            }
        }

        public void RemoveEntity(int index)
        {
            if (entityMap.ContainsKey(index) == true)
            {
                entitiList.Remove(entityMap[index]);
                entityMap.Remove(index);
            }
        }
    }
}
