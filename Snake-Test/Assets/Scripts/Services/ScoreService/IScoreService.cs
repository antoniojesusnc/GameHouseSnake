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
    public interface IScoreService : IService
    {
        EventHandler OnHighscoreChanged { get; set; }
        int GetScore();
        int GetHighscore();
        void AddScore();
        bool TrySetNewHighscore();
    }
}