using System.Collections.Generic;
using GameHouse.Snake.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace GameHouse.Snake.Services
{
    public class PoolService : IPoolService
    {
        private Dictionary<PoolTypes, IObjectPool<GameObject>> _poolObject = new ();
        
        public void Init()
        {
           
        }

        public void InitObjectToPool(PoolTypes poolType, Object gameObject, int initialValue)
        {
            _poolObject.Add(poolType, new ObjectPool<GameObject>(() => CreatePoolItem(gameObject as GameObject), defaultCapacity: initialValue));   
        }
        private GameObject CreatePoolItem(GameObject gameObject)
        {
            return GameObject.Instantiate(gameObject);
        }

        public bool TryGetObjectToPool(PoolTypes poolType, out GameObject gameObject)
        {
            if (!_poolObject.TryGetValue(poolType, out var objectPool))
            {
                Debug.LogWarning($"[PoolService] pool object of type {poolType} not found in the pool");
                gameObject = null;
                return false;
            }
            
            gameObject = objectPool.Get();
            return true;
        }

        public void ReleaseObjectToPool(PoolTypes poolType, GameObject gameObject)
        {
            if (!_poolObject.TryGetValue(poolType, out var objectPool))
            {
                Debug.LogWarning($"[PoolService] pool object of type {poolType} not found in the pool");
                gameObject = null;
                return;
            }
            
            objectPool.Release(gameObject);
        }
    }
}