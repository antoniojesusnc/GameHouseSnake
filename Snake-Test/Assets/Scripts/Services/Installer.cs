using GameHouse.Snake.Pool;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class Installer : MonoBehaviour
    {
        [Header("Pool Objects")]
        [SerializeField] 
        private GamePlay.Snake _snakePrefab;
        [SerializeField] 
        private GameObject _snakeBodyPrefab;
        
        [SerializeField] 
        private GameObject _foodPrefab;
        
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
            poolService.InitObjectToPool(PoolTypes.Snake, _snakePrefab.gameObject, 1);
            poolService.InitObjectToPool(PoolTypes.SnakeBody, _snakeBodyPrefab, 10);
            poolService.InitObjectToPool(PoolTypes.Food, _foodPrefab, 1);
        }
    }
}