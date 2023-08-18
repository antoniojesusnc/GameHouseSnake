using GameHouse.Snake.Services;
using UnityEngine;

namespace GameHouse.Snake.Loader
{
    public class LoaderCallback : MonoBehaviour
    {

        private bool firstUpdate = true;

        private void Update()
        {
            if (firstUpdate)
            {
                firstUpdate = false;
                ServiceLocator.GetService<ILoaderService>().LoaderCallback();
            }
        }
    }
}