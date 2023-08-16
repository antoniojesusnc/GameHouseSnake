using GameHouse.Snake.GamePlay;

namespace GameHouse.Snake.Services
{
    public interface IGamePlayService : IService
    {
        FoodSpawnerModule FoodSpawnerModule { get; }
        LevelGridModule LevelGridModule { get; }
        
        GamePlay.Snake Snake { get; }
        
        void ResumeGame();
        
        void SnakeDied();
        void BeginLevel();
    }
}
