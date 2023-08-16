using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public interface ISnakeMovePosition
    {
        public SnakeDirectionTypes DirectionTypes { get; }
        Vector2Int GetGridPosition();
        SnakeDirectionTypes GetDirection();
        SnakeDirectionTypes GetPreviousDirection();
    }
}