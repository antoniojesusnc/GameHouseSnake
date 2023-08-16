using GameHouse.Snake.Services;
using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public class FoodSpawnerModule
    {
        private Vector2Int foodGridPosition;
        private GameObject foodGameObject;

        private IGamePlayService _gamePlayService;
        
        public FoodSpawnerModule()
        {
            _gamePlayService = ServiceLocator.GetService<IGamePlayService>();
        }
        
        public void BeginGame()
        {
            SpawnFood();
        }
        
        public void SpawnFood() {
            do {
                foodGridPosition = new Vector2Int(Random.Range(0, _gamePlayService.LevelGridModule.Width), Random.Range(0, _gamePlayService.LevelGridModule.Height));
            } while (_gamePlayService.Snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

            foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
            foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        }
        
        public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
            if (snakeGridPosition == foodGridPosition) {
                Object.Destroy(foodGameObject);
                SpawnFood();
                Score.AddScore();
                return true;
            } else {
                return false;
            }
        }
    }
}