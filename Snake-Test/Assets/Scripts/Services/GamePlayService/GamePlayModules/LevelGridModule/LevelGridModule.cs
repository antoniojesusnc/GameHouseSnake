using UnityEngine;

public class LevelGridModule : ILevelGridModule
{

    public int Width { get; private set; }
    public int Height { get; private set; }

    public LevelGridModule()
    {
        
    }

    public void Setup(int width, int height) {
        Width = width;
        Height = height;
    }
    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if (gridPosition.x < 0) {
            gridPosition.x = Width - 1;
        }
        if (gridPosition.x > Width - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = Height - 1;
        }
        if (gridPosition.y > Height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
