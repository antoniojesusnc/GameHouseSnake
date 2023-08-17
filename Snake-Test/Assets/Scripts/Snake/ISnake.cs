
using System.Collections.Generic;
using GameHouse.Snake.Config;
using UnityEngine;

namespace GameHouse.Snake.GamePlay
{
    public interface ISnake
    {
        void Init(GamePlayConfig gamePlayConfig);
        void HandleInput();
        void HandleGridMovement();
        void CreateSnakeBodyPart();
        float GetAngleFromVector(Vector2Int dir);
        Vector2Int GetGridPosition();
        List<Vector2Int> GetFullSnakeGridPositionList();
        bool useGUILayout { get; set; }
    }
}
