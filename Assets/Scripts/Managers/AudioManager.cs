using System;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _musicSource;
        
        [SerializeField] private float _sfxVolume = 1f;
        [SerializeField] private float _musicVolume = 0.5f;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return; 
            }
            _sfxSource ??= gameObject.AddComponent<AudioSource>();
        
            _musicSource ??= gameObject.AddComponent<AudioSource>();
        
            _sfxSource.volume = _sfxVolume;
            _musicSource.volume = _musicVolume;
            _musicSource.loop = true;
        }
        
        public void PlaySfx(AudioClip clip)
        {
            if (clip is null) return;
        
            _sfxSource.PlayOneShot(clip, _sfxVolume);
        }
        
        public void PlayMusic(AudioClip clip)
        {
            if (clip is null) return;
        
            _musicSource.clip = clip;
            _musicSource.Play();
        }
        
        public void SetSfxVolume(float volume)
        {
            _sfxVolume = Mathf.Clamp01(volume);
            _sfxSource.volume = _sfxVolume;
        }
        
        
        public void SetMusicVolume(float volume)
        {
            _musicVolume = Mathf.Clamp01(volume);
            _musicSource.volume = _musicVolume;
        }

        
        
    }
}