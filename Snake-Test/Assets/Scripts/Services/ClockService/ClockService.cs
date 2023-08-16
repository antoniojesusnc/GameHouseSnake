using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class ClockService : IClockService
    {
        private const string CLOCK_SERVICE_GAMEOBJECT_NAME = "ClockServiceGameObject";
        
        private ClockServiceGameObject _clockServiceGameObject;
        public event IClockService.OnUpdateDelegate OnUpdate;
        
        public bool IsGamePaused { get; private set; }
        
        public void Init()
        {
            _clockServiceGameObject = new GameObject(CLOCK_SERVICE_GAMEOBJECT_NAME).AddComponent<ClockServiceGameObject>();
            _clockServiceGameObject.SetClockService(this);
        }

        public void SetPause(bool pauseGame)
        {
            IsGamePaused = pauseGame;
        }

        public void CallUpdate(float deltaTime)
        {
            if (IsGamePaused)
            {
                return;
            }
            
            OnUpdate?.Invoke(deltaTime);
        }
    }
}