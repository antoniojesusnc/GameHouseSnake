using GameHouse.Snake.Config;
using GameHouse.Snake.Pool;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameHouse.Snake.Services
{
    public class Installer : MonoBehaviour
    {
        [Header("Pool Objects"), SerializeField] 
        private InitialPoolConfig _poolConfig;
        
        void Awake()
        {
            ServiceLocator.RegisterService<ISoundService>(new SoundService());
            ServiceLocator.RegisterService<IClockService>(new ClockService());
            ServiceLocator.RegisterService<IPoolService>(new PoolService());
            InitPoolItems();
            
            
            ServiceLocator.RegisterService<IScoreService>(new ScoreService());
            ServiceLocator.RegisterService<ILoaderService>(new LoaderService());
            ServiceLocator.RegisterService<IGamePlayService>(new GamePlayService());
        }

        private void InitPoolItems()
        {
            var poolService = ServiceLocator.GetService<IPoolService>();
            
            if (_poolConfig.PoolItemsFromAddressable?.Count > 0)
            {
                for (int i = 0; i < _poolConfig.PoolItemsFromAddressable.Count; i++)
                {
                    var poolItem = _poolConfig.PoolItemsFromAddressable[i];
                    
                    poolItem.Reference.LoadAssetAsync<GameObject>().Completed += 
                        (operationHandle => poolService.InitObjectToPool(poolItem.PoolType, poolItem.Reference.Asset, poolItem.InitialAmount));
                }
            }
            
            if (_poolConfig.PoolItemsGameObject?.Count > 0)
            {
                for (int i = 0; i < _poolConfig.PoolItemsGameObject.Count; i++)
                {
                    var poolItem = _poolConfig.PoolItemsGameObject[i];
                    poolService.InitObjectToPool(poolItem.PoolType, poolItem.Reference, poolItem.InitialAmount);
                }
            }

        }
    }
}