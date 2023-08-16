/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class ScoreService : IScoreService
    {
        public event EventHandler OnHighscoreChanged;

        private static int score;
        private EventHandler _onHighscoreChanged;

        public void Init()
        {
            OnHighscoreChanged = null;
            score = 0;
        }
        
        EventHandler IScoreService.OnHighscoreChanged
        {
            get => _onHighscoreChanged;
            set => _onHighscoreChanged = value;
        }

        public int GetScore()
        {
            return score;
        }

        public void AddScore()
        {
            score += 100;
        }

        public int GetHighscore()
        {
            return PlayerPrefs.GetInt("highscore", 0);
        }

        public bool TrySetNewHighscore()
        {
            return TrySetNewHighscore(score);
        }

        public bool TrySetNewHighscore(int score)
        {
            int highscore = GetHighscore();
            if (score > highscore)
            {
                PlayerPrefs.SetInt("highscore", score);
                PlayerPrefs.Save();
                if (OnHighscoreChanged != null) OnHighscoreChanged(null, EventArgs.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}