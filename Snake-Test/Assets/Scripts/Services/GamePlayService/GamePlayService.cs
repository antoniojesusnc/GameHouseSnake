using GameHouse.Snake.GamePlay;
using GameHouse.Snake.Pool;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class GamePlayService : IGamePlayService
    {
        public ILevelGridModule LevelGridModule { get; private set; }
        public IFoodSpawnerModule FoodSpawnerModule { get; private set; }

        public ISnake Snake { get; private set; }

        private IClockService _clockService;
        public void Init()
        {
            _clockService = ServiceLocator.GetService<IClockService>();
        }

        public void BeginLevel()
        {
            LevelGridModule = new LevelGridModule();
            FoodSpawnerModule = new FoodSpawnerModule();

            LevelGridModule.Setup(20, 20);
            
            InstantiateSnake();
            
            FoodSpawnerModule.BeginGame();
            
            ServiceLocator.GetService<IClockService>().OnUpdate += OnUpdate;
        }

        public void EndLevel()
        {
            ServiceLocator.GetService<IClockService>().OnUpdate -= OnUpdate;
        }

        private void InstantiateSnake()
        {
            if (!ServiceLocator.GetService<IPoolService>().TryGetObjectToPool(PoolTypes.Snake, out var snake))
            {
                Debug.LogError("No Object Snake found in the Pool");
                return;
            }
            
            Snake = snake.GetComponent<ISnake>();
        }
        
        private void OnUpdate(float deltatime)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (_clockService.IsGamePaused) {
                    ResumeGame();
                } else {
                    PauseGame();
                }
            }
        }
        
        public void ResumeGame() {
            PauseWindow.HideStatic();
            _clockService.SetPause(false);
        }

        private void PauseGame() {
            PauseWindow.ShowStatic();
            _clockService.SetPause(true);
        }
        
        public void SnakeDied() {
            bool isNewHighscore = ServiceLocator.GetService<IScoreService>().TrySetNewHighscore();
            GameOverWindow.ShowStatic(isNewHighscore);
            ScoreWindow.HideStatic();
        }
    }
}