using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _musicSource;
        
        [SerializeField] private float _sfxVolume = 1f;
        [SerializeField] private float _musicVolume = 0.5f;
        
        [Header("Scene Music Settings")]
        [SerializeField] private SceneMusicData[] _sceneMusicData;
        
        private Dictionary<string, AudioClip> _sceneMusicMap = new Dictionary<string, AudioClip>();
        
        [Serializable]
        public class SceneMusicData
        {
            public string sceneName;
            public AudioClip musicClip;
        }

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                InitializeSceneMusicMap();
                
                SceneManager.sceneLoaded += OnSceneLoaded;
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
        
        private void InitializeSceneMusicMap()
        {
            _sceneMusicMap.Clear();
            
            if (_sceneMusicData != null)
            {
                foreach (var data in _sceneMusicData)
                {
                    if (!string.IsNullOrEmpty(data.sceneName) && data.musicClip != null)
                    {
                        _sceneMusicMap[data.sceneName] = data.musicClip;
                    }
                }
            }
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // 새로운 씬이 로드되면 해당 씬에 맞는 배경음악 재생
            Debug.Log("Scene Loaded: " + scene.name);
            PlaySceneMusic(scene.name);
        }
        
        public void PlaySceneMusic(string sceneName)
        {
            if (_sceneMusicMap.TryGetValue(sceneName, out AudioClip musicClip))
            {
                PlayMusic(musicClip);
            }
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
        
        public void StopMusic()
        {
            _musicSource.Stop();
        }
        
        public void PauseMusic()
        {
            _musicSource.Pause();
        }
        
        public void ResumeMusic()
        {
            _musicSource.UnPause();
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
        
        public float GetSfxVolume()
        {
            return _sfxVolume;
        }
        
        public float GetMusicVolume()
        {
            return _musicVolume;
        }
        
        private void OnDestroy()
        {
            // 이벤트 리스너 제거
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}