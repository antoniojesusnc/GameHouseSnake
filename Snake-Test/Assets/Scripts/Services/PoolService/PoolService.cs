using System.Collections.Generic;
using GameHouse.Snake.Config;
using GameHouse.Snake.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace GameHouse.Snake.Services
{
    public class PoolService : IPoolService
    {
        private const string INITIAL_POOL_ADDRESS_NAME = "InitialPoolConfig";

        private Dictionary<PoolTypes, IObjectPool<GameObject>> _poolObject = new();

        public void Init()
        {
            ServiceLocator.GetService<IAssetService>()
                          .LoadWithAddress<InitialPoolConfig>(INITIAL_POOL_ADDRESS_NAME, OnLoadInitialPoolData);
        }

        private void OnLoadInitialPoolData(InitialPoolConfig initialPoolConfig)
        {
            if (initialPoolConfig.PoolItemsFromAddressable?.Count > 0)
            {
                for (int i = 0; i < initialPoolConfig.PoolItemsFromAddressable.Count; i++)
                {
                    var poolItem = initialPoolConfig.PoolItemsFromAddressable[i];

                    poolItem.Reference.LoadAssetAsync<GameObject>().Completed +=
                        (operationHandle =>
                            InitObjectToPool(poolItem.PoolType, poolItem.Reference.Asset,
                                                               poolItem.InitialAmount));
                }
            }

            if (initialPoolConfig.PoolItemsGameObject?.Count > 0)
            {
                for (int i = 0; i < initialPoolConfig.PoolItemsGameObject.Count; i++)
                {
                    var poolItem = initialPoolConfig.PoolItemsGameObject[i];
                    InitObjectToPool(poolItem.PoolType, poolItem.Reference, poolItem.InitialAmount);
                }
            }
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