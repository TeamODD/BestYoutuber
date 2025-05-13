using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Fade UI")] 
        [SerializeField] private Image _fadeImage;
    
        [Header("Settings")]
        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private float _postLoadDelay = 0.1f;
    
        private static SceneLoader _instance;
        private bool _isLoading = false;
        public static SceneLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SceneLoader>();
                    if (_instance == null)
                        Debug.LogError("SceneLoader instance not found in scene.");
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _fadeImage.gameObject.SetActive(false);
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, 0f);
        }
    
        public static void LoadScene(string sceneName)
        {
            Instance.InternalLoadScene(sceneName);
        }
    
        public static void LoadScene(int sceneIndex)
        {
            Instance.InternalLoadScene(sceneIndex);
        }

        private void InternalLoadScene(string sceneName)
        {
            if (_isLoading) return;
            StartCoroutine(LoadSceneAsyncRoutine(() => SceneManager.LoadSceneAsync(sceneName)));
        }

        private void InternalLoadScene(int sceneIndex)
        {
            if (_isLoading) return;
            StartCoroutine(LoadSceneAsyncRoutine(() => SceneManager.LoadSceneAsync(sceneIndex)));
        }
    
        private IEnumerator LoadSceneAsyncRoutine(System.Func<AsyncOperation> loadOpFactory)
        {
            _isLoading = true;

            yield return StartCoroutine(FadeCoroutine(0f, 1f, _fadeTime));

            AsyncOperation op = loadOpFactory();
            op.allowSceneActivation = false;

            while (op.progress < 0.9f)
                yield return null;

            yield return new WaitForSecondsRealtime(_postLoadDelay);

            op.allowSceneActivation = true;
            yield return null;

            yield return StartCoroutine(FadeCoroutine(1f, 0f, _fadeTime));

            _isLoading = false;
        }
    
        private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration)
        {
            _fadeImage.gameObject.SetActive(true);

            float elapsed = 0f;
            Color c = _fadeImage.color;
            c.a = startAlpha;
            _fadeImage.color = c;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                c.a = Mathf.Lerp(startAlpha, endAlpha, t);
                _fadeImage.color = c;

                yield return null;
            }

            c.a = endAlpha;
            _fadeImage.color = c;

            if (endAlpha <= 0f)
                _fadeImage.gameObject.SetActive(false);
        }
    
    }
}
