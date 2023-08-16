using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public interface IFoodSpawnerModule
    {
        void BeginGame();
        void SpawnFood();
        bool TrySnakeEatFood(Vector2Int snakeGridPosition);
    }
}