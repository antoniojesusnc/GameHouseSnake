using UnityEngine;

namespace GameHouse.Snake.Services
{
    public class Installer : MonoBehaviour
    {
        void Awake()
        {
            ServiceLocator.RegisterService<ISoundService>(new SoundService());
        }
    }
}