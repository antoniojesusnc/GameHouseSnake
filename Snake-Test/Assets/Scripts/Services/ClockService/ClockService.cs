namespace GameHouse.Snake.Services
{
    public class ClockService : IClockService
    {
        public event IClockService.OnUpdateDelegate OnUpdate;
        
        public bool IsGamePaused { get; private set; }
        
        public void Init()
        {
            
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