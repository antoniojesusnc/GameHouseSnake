using GameHouse.Snake.GamePlay;

namespace GameHouse.Snake.Services
{
    public interface IGamePlayService : IService
    {
        IFoodSpawnerModule FoodSpawnerModule { get; }
        ILevelGridModule LevelGridModule { get; }
        
        ISnake Snake { get; }
        
        void ResumeGame();
        
        void SnakeDied();
        void BeginLevel();
    }
}
