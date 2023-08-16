using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameHouse.Snake.Sounds
{
    public class SoundServiceGameObject : MonoBehaviour
    {
        private List<AudioSource> _audioSources = new List<AudioSource>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public AudioSource GetOrInstantiateAudioSource()
        {
            var audioSource = _audioSources.Find(audioSources => !audioSources.isPlaying);
            if (audioSource != null)
            {
                return audioSource;
            }

            audioSource = CreateNewAudioSource();
            _audioSources.Add(audioSource);
            return audioSource;
        }

        private AudioSource CreateNewAudioSource()
        {
            return gameObject.AddComponent<AudioSource>();
        }
    }
}