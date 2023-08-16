using UnityEngine;

public interface ILevelGridModule
{
    int Width { get; }
    int Height { get; }
    void Setup(int width, int height);
    Vector2Int ValidateGridPosition(Vector2Int gridPosition);
}