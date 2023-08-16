using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public interface ISnakeBodyPart
    {
        void SetSnakeMovePosition(ISnakeMovePosition snakeMovePosition);
        Vector2Int GetGridPosition();
    }
}