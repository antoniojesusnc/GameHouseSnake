using System;

namespace GameHouse.Snake.Services
{
    public interface IAssetService : IService
    {
        void LoadWithAddress<T>(string address, Action<T> onLoadCallback) where T : class;
    }
}