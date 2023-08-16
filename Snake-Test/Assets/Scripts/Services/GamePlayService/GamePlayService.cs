using GameHouse.Snake.GamePlay;
using GameHouse.Snake.Pool;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class GamePlayService : IGamePlayService
    {
        public LevelGridModule LevelGridModule { get; private set; }
        public FoodSpawnerModule FoodSpawnerModule { get; private set; }

        public GamePlay.Snake Snake { get; private set; }

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
            }
            
            Snake = snake.GetComponent<GamePlay.Snake>();
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