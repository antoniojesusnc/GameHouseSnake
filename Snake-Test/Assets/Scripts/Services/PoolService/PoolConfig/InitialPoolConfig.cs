using System;
using System.Collections.Generic;
using GameHouse.Snake.Pool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameHouse.Snake.Config
{
    [CreateAssetMenu(fileName = "InitialPoolConfig", menuName = "GameHouse/InitialPoolConfig", order = 1)]
    public class InitialPoolConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<InitialPoolConfigType<AssetReference>> PoolItemsFromAddressable { get; private set; }
        
        [field: SerializeField]
        public List<InitialPoolConfigType<GameObject>> PoolItemsGameObject { get; private set; }

        [Serializable]
        public class InitialPoolConfigType<T>
        {
            [field: SerializeField]
            public PoolTypes PoolType { get; private set; }
            [field: SerializeField]
            public T Reference { get; private set; }
            [field: SerializeField]
            public int InitialAmount { get; private set; }
        }
    }
}
