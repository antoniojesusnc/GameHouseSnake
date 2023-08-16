using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GameHouse.Snake.Services
{
    public class PoolService : IPoolService
    {
        private List<IObjectPool<GameObject>> _pool = new List<IObjectPool<GameObject>>();
        
        public void Init()
        {
            
        }

        public void AddObjectToPool(GameObject gameObject, int initialValue)
        {
            _pool.Add(new ObjectPool<GameObject>(() => CreatePoolItem(gameObject), defaultCapacity: initialValue));   
        }

        private GameObject CreatePoolItem(GameObject gameObject)
        {
            return GameObject.Instantiate(gameObject);
        }
    }
}