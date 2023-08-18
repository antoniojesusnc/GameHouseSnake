using System;

namespace GameHouse.Snake.Services
{
    public interface IScoreService : IService
    {
        EventHandler OnHighscoreChanged { get; set; }
        int GetScore();
        int GetHighscore();
        void AddScore();
        bool TrySetNewHighscore();
    }
}