using GameHouse.Snake.Config;
using GameHouse.Snake.GamePlay;
using GameHouse.Snake.Pool;
using GameHouse.Snake.UI;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class GamePlayService : IGamePlayService
    {
        private const string GAME_CONFIG_ADDRESS_NAME = "GamePlayConfig";
        
        public ILevelGridModule LevelGridModule { get; private set; }
        public IFoodSpawnerModule FoodSpawnerModule { get; private set; }

        public ISnake Snake { get; private set; }

        private IClockService _clockService;

        private GamePlayConfig _gamePlayConfig;
        
        public void Init()
        {
            _clockService = ServiceLocator.GetService<IClockService>();
            
            ServiceLocator.GetService<IAssetService>().
                           LoadWithAddress<GamePlayConfig>(GAME_CONFIG_ADDRESS_NAME, (config) => _gamePlayConfig = config);
        }

        public void BeginLevel()
        {
            LevelGridModule = new LevelGridModule();
            FoodSpawnerModule = new FoodSpawnerModule();

            LevelGridModule.Setup(_gamePlayConfig.GridSize.x, _gamePlayConfig.GridSize.y);
            
            InstantiateSnake();
            Snake.Init(_gamePlayConfig);
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