using GameHouse.Snake.GamePlay;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class GamePlayService : IGamePlayService
    {
        private LevelGridModule _levelGridModule = new LevelGridModule();
        private FoodSpawnerModule _foodSpawnerModule = new FoodSpawnerModule();

        private GamePlay.Snake _snake;

        private IClockService _clockService;
        public void Init()
        {
            _clockService = ServiceLocator.GetService<IClockService>();
            
            _levelGridModule = new LevelGridModule();
            Score.InitializeStatic();
            Time.timeScale = 1f;
        }

        public void BeginLevel()
        {
            InstantiateSnake();
            _levelGridModule.Setup(20, 20, _snake);
            _snake.Setup(_levelGridModule);
            
            ServiceLocator.GetService<IClockService>().OnUpdate += OnUpdate;
        }

        public void EndLevel()
        {
            ServiceLocator.GetService<IClockService>().OnUpdate -= OnUpdate;
        }

        private void InstantiateSnake()
        {
            var snake = ServiceLocator.GetService<IPoolService>().InitObjectToPool()
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

        public void PauseGame() {
            PauseWindow.ShowStatic();
            _clockService.SetPause(true);
        }
        
        public static void SnakeDied() {
            bool isNewHighscore = Score.TrySetNewHighscore();
            GameOverWindow.ShowStatic(isNewHighscore);
            ScoreWindow.HideStatic();
        }
    }
}