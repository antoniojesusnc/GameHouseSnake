using UnityEngine;
using GameHouse.Snake.Config;
using GameHouse.Snake.Sounds;

namespace GameHouse.Snake.Services
{
    public class SoundService : ISoundService
    {
        private const string SOUND_SERVICE_GAMEOBJECT_NAME = "SoundServiceGameObject";
        private const string SOUND_CONFIG_ADDRESS_NAME = "SoundConfig";

        private SoundServiceGameObject _soundServiceGameObject;
        private SoundConfig _soundConfig;

        public void Init()
        {
            _soundServiceGameObject = new GameObject(SOUND_SERVICE_GAMEOBJECT_NAME).AddComponent<SoundServiceGameObject>();

            PreloadSounds();
        }

        private void PreloadSounds()
        {
            ServiceLocator.GetService<IAssetService>()
                          .LoadWithAddress<SoundConfig>(SOUND_CONFIG_ADDRESS_NAME, OnLoadSoundConfig);
        }

        private void OnLoadSoundConfig(SoundConfig soundConfig)
        {
            _soundConfig = soundConfig;

            for (int i = 0; i < _soundConfig.SoundAudioClipArray.Count; i++)
            {
                var sound = _soundConfig.SoundAudioClipArray[i];
                sound.AudioClip.LoadAssetAsync();
            }
        }
        
        public void PlaySound(SoundTypes soundTypes)
        {
            if(!TryGetAudioClip(soundTypes, out AudioClip audioClip))
            {
                Debug.LogWarning($"[SoundService] Audio clip for sound {soundTypes} not found");
                return;
            }

            AudioSource audioSource = _soundServiceGameObject.GetOrInstantiateAudioSource();
            audioSource.PlayOneShot(audioClip);
        }

        private bool TryGetAudioClip(SoundTypes soundType, out AudioClip audioClip)
        {
            var soundConfig = _soundConfig.SoundAudioClipArray.Find(sound => sound.Sound == soundType);
            if (soundConfig == null)
            {
                audioClip = null;
                return false;
            }
            
            audioClip = soundConfig.AudioClip.Asset as AudioClip;
            return true;
        }
    }
}