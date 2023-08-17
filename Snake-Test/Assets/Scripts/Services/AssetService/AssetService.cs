using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace GameHouse.Snake.Services
{
    public class AssetService : IAssetService
    {
        public void Init()
        {
        }
        
        public void LoadWithAddress<T>(string address, Action<T> onLoadCallback) where T : class
        {
            Addressables.LoadAssetAsync<T>(address).Completed += (operation) => Handle_Completed(address, operation, onLoadCallback);
        }

        private void Handle_Completed<T>(string address, AsyncOperationHandle<T> operation, Action<T> onLoadCallback) where T: class
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                if (typeof(T) == typeof(GameObject))
                {
                    onLoadCallback?.Invoke(GameObject.Instantiate(operation.Result as GameObject) as T);
                }
                else
                {
                    onLoadCallback?.Invoke(operation.Result);
                }
            }
            else
            {
                Debug.LogError($"Asset for {address} failed to load.");
            }
        }
    }
}
