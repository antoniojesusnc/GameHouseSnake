using GameHouse.Snake.Scenes;

namespace GameHouse.Snake.Services
{
    public interface ILoaderService : IService
    {
        void LoaderCallback();
        void Load(SceneTypes gameScene);
    }
}