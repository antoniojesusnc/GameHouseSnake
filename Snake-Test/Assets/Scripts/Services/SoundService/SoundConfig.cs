using System;
using System.Collections.Generic;
using GameHouse.Snake.Sounds;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameHouse.Snake.Config
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "GameHouse/SoundConfig", order = 1)]
    public class SoundConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<SoundAudioClip> SoundAudioClipArray { get; private set; }

        [Serializable]
        public class SoundAudioClip {
            [field: SerializeField]
            public SoundTypes Sound { get; private set; }
            [field: SerializeField]
            public AssetReferenceT<AudioClip> AudioClip { get; private set; }
        }
    }
}
