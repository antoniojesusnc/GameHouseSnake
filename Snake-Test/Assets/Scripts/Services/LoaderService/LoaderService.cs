using System;
using GameHouse.Snake.Scenes;
using UnityEngine.SceneManagement;

namespace GameHouse.Snake.Services
{
    public class LoaderService : ILoaderService
    {
        private Action loaderCallbackAction;

        public void Init()
        {
            
        }
        public void Load(SceneTypes scene)
        {
            // Set up the callback action that will be triggered after the Loading scene is loaded
            loaderCallbackAction = () =>
            {
                // Load target scene when the Loading scene is loaded
                SceneManager.LoadScene(scene.ToString());
            };

            // Load loading scene
            SceneManager.LoadScene(SceneTypes.Loading.ToString());
        }

        public void LoaderCallback()
        {
            if (loaderCallbackAction != null)
            {
                loaderCallbackAction();
                loaderCallbackAction = null;
            }
        }
    }
}