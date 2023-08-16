using GameHouse.Snake.Sounds;

namespace GameHouse.Snake.Services
{
    public interface ISoundService : IService
    {
        public void PlaySound(SoundTypes soundTypes);
    }
}