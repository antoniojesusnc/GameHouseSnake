using UnityEngine;

namespace GameHouse.Snake.Services
{
    public interface IPoolService : IService
    {
        void AddObjectToPool(GameObject gameObject, int initialValue);
    }
}