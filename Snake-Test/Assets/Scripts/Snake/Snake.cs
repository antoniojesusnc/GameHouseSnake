/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using GameHouse.Snake.Services;
using GameHouse.Snake.Sounds;

namespace GameHouse.Snake.GamePlay
{
    public partial class Snake : MonoBehaviour, ISnake
    {
        private enum State
        {
            Alive,
            Dead
        }

        private State state;
        private SnakeDirectionTypes _gridMoveSnakeDirectionTypes;
        private Vector2Int gridPosition;
        private float gridMoveTimer;
        private float gridMoveTimerMax;
        
        
        private int snakeBodySize;
        private List<ISnakeMovePosition> snakeMovePositionList;
        private List<ISnakeBodyPart> snakeBodyPartList;

        private ISoundService _soundService;
        private ILevelGridModule _levelGridModule;
        private IFoodSpawnerModule _foodSpawnerModule;

        private void Awake()
        {
            _soundService = ServiceLocator.GetService<ISoundService>();

            gridPosition = new Vector2Int(10, 10);
            gridMoveTimerMax = .2f;
            gridMoveTimer = gridMoveTimerMax;
            _gridMoveSnakeDirectionTypes = SnakeDirectionTypes.Right;

            snakeMovePositionList = new List<ISnakeMovePosition>();
            snakeBodySize = 0;

            snakeBodyPartList = new List<ISnakeBodyPart>();

            state = State.Alive;

            _levelGridModule = ServiceLocator.GetService<IGamePlayService>().LevelGridModule;
            _foodSpawnerModule = ServiceLocator.GetService<IGamePlayService>().FoodSpawnerModule;
        }

        private void Update()
        {
            switch (state)
            {
                case State.Alive:
                    HandleInput();
                    HandleGridMovement();
                    break;
                case State.Dead:
                    break;
            }
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_gridMoveSnakeDirectionTypes != SnakeDirectionTypes.Down)
                {
                    _gridMoveSnakeDirectionTypes = SnakeDirectionTypes.Up;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_gridMoveSnakeDirectionTypes != SnakeDirectionTypes.Up)
                {
                    _gridMoveSnakeDirectionTypes = SnakeDirectionTypes.Down;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_gridMoveSnakeDirectionTypes != SnakeDirectionTypes.Right)
                {
                    _gridMoveSnakeDirectionTypes = SnakeDirectionTypes.Left;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_gridMoveSnakeDirectionTypes != SnakeDirectionTypes.Left)
                {
                    _gridMoveSnakeDirectionTypes = SnakeDirectionTypes.Right;
                }
            }
        }

        public void HandleGridMovement()
        {
            gridMoveTimer += Time.deltaTime;
            if (gridMoveTimer >= gridMoveTimerMax)
            {
                gridMoveTimer -= gridMoveTimerMax;

                //SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

                ISnakeMovePosition previousSnakeMovePosition = null;
                if (snakeMovePositionList.Count > 0)
                {
                    previousSnakeMovePosition = snakeMovePositionList[0];
                }

                ISnakeMovePosition snakeMovePosition =
                    new SnakeMovePosition(previousSnakeMovePosition, gridPosition, _gridMoveSnakeDirectionTypes);
                snakeMovePositionList.Insert(0, snakeMovePosition);

                Vector2Int gridMoveDirectionVector;
                switch (_gridMoveSnakeDirectionTypes)
                {
                    default:
                    case SnakeDirectionTypes.Right:
                        gridMoveDirectionVector = new Vector2Int(+1, 0);
                        break;
                    case SnakeDirectionTypes.Left:
                        gridMoveDirectionVector = new Vector2Int(-1, 0);
                        break;
                    case SnakeDirectionTypes.Up:
                        gridMoveDirectionVector = new Vector2Int(0, +1);
                        break;
                    case SnakeDirectionTypes.Down:
                        gridMoveDirectionVector = new Vector2Int(0, -1);
                        break;
                }

                gridPosition += gridMoveDirectionVector;

                gridPosition = _levelGridModule.ValidateGridPosition(gridPosition);

                bool snakeAteFood = _foodSpawnerModule.TrySnakeEatFood(gridPosition);
                if (snakeAteFood)
                {
                    // Snake ate food, grow body
                    snakeBodySize++;
                    CreateSnakeBodyPart();
                    _soundService.PlaySound(SoundTypes.SnakeEat);
                }

                if (snakeMovePositionList.Count >= snakeBodySize + 1)
                {
                    snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
                }

                UpdateSnakeBodyParts();

                foreach (ISnakeBodyPart snakeBodyPart in snakeBodyPartList)
                {
                    Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                    if (gridPosition == snakeBodyPartGridPosition)
                    {
                        // Game Over!
                        //CMDebug.TextPopup("DEAD!", transform.position);
                        state = State.Dead;
                        ServiceLocator.GetService<IGamePlayService>().SnakeDied();
                        _soundService.PlaySound(SoundTypes.SnakeDie);
                    }
                }

                transform.position = new Vector3(gridPosition.x, gridPosition.y);
                transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);
            }
        }

        public void CreateSnakeBodyPart()
        {
            snakeBodyPartList.Add(new SnakeBodyPart());
        }

        private void UpdateSnakeBodyParts()
        {
            for (int i = 0; i < snakeBodyPartList.Count; i++)
            {
                snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
            }
        }


        public float GetAngleFromVector(Vector2Int dir)
        {
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            return n;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        // Return the full list of positions occupied by the snake: Head + Body
        public List<Vector2Int> GetFullSnakeGridPositionList()
        {
            List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
            foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
            {
                gridPositionList.Add(snakeMovePosition.GetGridPosition());
            }

            return gridPositionList;
        }
    }
}