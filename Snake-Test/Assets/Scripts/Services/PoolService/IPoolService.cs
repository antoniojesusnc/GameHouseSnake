using GameHouse.Snake.Pool;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public interface IPoolService : IService
    {
        void InitObjectToPool(PoolTypes poolType, Object gameObject, int initialValue);
        public bool TryGetObjectToPool(PoolTypes poolType, out GameObject gameObject);
        void ReleaseObjectToPool(PoolTypes poolType, GameObject gameObject);
    }
}