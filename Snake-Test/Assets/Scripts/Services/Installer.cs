using GameHouse.Snake.Config;
using GameHouse.Snake.Pool;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameHouse.Snake.Services
{
    public class Installer : MonoBehaviour
    {
        void Awake()
        {
            ServiceLocator.RegisterService<IClockService>(new ClockService());
            ServiceLocator.RegisterService<IAssetService>(new AssetService());
            ServiceLocator.RegisterService<ISoundService>(new SoundService());
            ServiceLocator.RegisterService<IPoolService>(new PoolService());

            ServiceLocator.RegisterService<IScoreService>(new ScoreService());
            ServiceLocator.RegisterService<ILoaderService>(new LoaderService());
            ServiceLocator.RegisterService<IGamePlayService>(new GamePlayService());
        }

        
    }
}