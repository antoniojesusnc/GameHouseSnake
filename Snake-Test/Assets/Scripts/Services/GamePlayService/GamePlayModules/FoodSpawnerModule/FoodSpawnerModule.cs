using GameHouse.Snake.Pool;
using GameHouse.Snake.Services;
using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public class FoodSpawnerModule : IFoodSpawnerModule
    {
        private Vector2Int foodGridPosition;
        private GameObject foodGameObject;

        private IGamePlayService _gamePlayService;
        private IScoreService _scoreService;
        
        public FoodSpawnerModule()
        {
            _gamePlayService = ServiceLocator.GetService<IGamePlayService>();
            _scoreService = ServiceLocator.GetService<IScoreService>();
        }
        
        public void BeginGame()
        {
            SpawnFood();
        }
        
        public void SpawnFood()
        {

            if (!ServiceLocator.GetService<IPoolService>().TryGetObjectToPool(PoolTypes.Food, out foodGameObject))
            {
                Debug.LogError("No Object Snake found in the Pool");
                return;
            }
            foodGridPosition = GetPosition();
            foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        }

        private Vector2Int GetPosition()
        {
            Vector2Int newFoodPosition;
            do {
                newFoodPosition = new Vector2Int(Random.Range(0, _gamePlayService.LevelGridModule.Width), Random.Range(0, _gamePlayService.LevelGridModule.Height));
            } while (_gamePlayService.Snake.GetFullSnakeGridPositionList().IndexOf(newFoodPosition) != -1);

            return newFoodPosition;
        }

        public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
            if (snakeGridPosition == foodGridPosition) {
                Object.Destroy(foodGameObject);
                SpawnFood();
                _scoreService.AddScore();
                return true;
            } else {
                return false;
            }
        }
    }
}