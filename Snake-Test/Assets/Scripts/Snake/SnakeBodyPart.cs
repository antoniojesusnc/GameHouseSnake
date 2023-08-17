using GameHouse.Snake.Pool;
using GameHouse.Snake.Services;
using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public class SnakeBodyPart : ISnakeBodyPart
    {
        private readonly Vector3 BODY_PART_OFFSET_WHEN_TURN = new Vector3(.2f, .2f);
        
        private ISnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart()
        {
            if (!ServiceLocator.GetService<IPoolService>().TryGetObjectToPool(PoolTypes.SnakeBody, out var snakeBodyGameObject))
            {
                Debug.LogError("No Object Snake found in the Pool");
                return;
            }
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(ISnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;

            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x,
                                             snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case SnakeDirectionTypes.Up: // Currently going Up
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        case SnakeDirectionTypes.Left: // Previously was going Left
                            angle = 0 + 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                        case SnakeDirectionTypes.Right: // Previously was going Right
                            angle = 0 - 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                    }

                    break;
                case SnakeDirectionTypes.Down: // Currently going Down
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        case SnakeDirectionTypes.Left: // Previously was going Left
                            angle = 180 - 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                        case SnakeDirectionTypes.Right: // Previously was going Right
                            angle = 180 + 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                    }

                    break;
                case SnakeDirectionTypes.Left: // Currently going to the Left
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = +90;
                            break;
                        case SnakeDirectionTypes.Down: // Previously was going Down
                            angle = 180 - 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                        case SnakeDirectionTypes.Up: // Previously was going Up
                            angle = 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                    }

                    break;
                case SnakeDirectionTypes.Right: // Currently going to the Right
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        case SnakeDirectionTypes.Down: // Previously was going Down
                            angle = 180 + 45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                        case SnakeDirectionTypes.Up: // Previously was going Up
                            angle = -45;
                            transform.position += BODY_PART_OFFSET_WHEN_TURN;
                            break;
                    }

                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }
}