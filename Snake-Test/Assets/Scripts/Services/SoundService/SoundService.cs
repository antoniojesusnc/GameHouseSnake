/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;
using CodeMonkey.Utils;
using GameHouse.Snake.Sounds;

namespace GameHouse.Snake.Services
{
    public class SoundService : ISoundService
    {
        private const string SOUND_SERVICE_GAMEOBJECT_NAME = "SoundServiceGameObject";
        
        private SoundServiceGameObject _soundServiceGameObject;
        
        public void Init()
        {
            _soundServiceGameObject = new GameObject(SOUND_SERVICE_GAMEOBJECT_NAME).AddComponent<SoundServiceGameObject>();
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

        private bool TryGetAudioClip(SoundTypes soundTypes, out AudioClip audioClip)
        {
            foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
            {
                if (soundAudioClip.sound == soundTypes)
                {
                    audioClip = soundAudioClip.audioClip;
                    return true;
                }
            }
            
            audioClip = null;
            return false;
        }

       
       
    }
}