using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public class SnakeMovePosition : ISnakeMovePosition
    {
        private ISnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        public SnakeDirectionTypes DirectionTypes { get; private set; }

        public SnakeMovePosition(ISnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition,
            SnakeDirectionTypes directionTypes)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.DirectionTypes = directionTypes;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public SnakeDirectionTypes GetDirection()
        {
            return DirectionTypes;
        }

        public SnakeDirectionTypes GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return SnakeDirectionTypes.Right;
            }
            else
            {
                return previousSnakeMovePosition.DirectionTypes;
            }
        }

    }
}