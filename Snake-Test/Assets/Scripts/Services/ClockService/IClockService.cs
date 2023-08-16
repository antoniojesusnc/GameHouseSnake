namespace GameHouse.Snake.Services
{
    public interface IClockService : IService
    {
        public delegate void OnUpdateDelegate(float deltaTime);
        public event OnUpdateDelegate OnUpdate;
        
        public bool IsGamePaused { get; }
        public void SetPause(bool pauseGame);
    }
}