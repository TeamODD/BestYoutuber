using UnityEngine;

public class EndingSceneEffect : MonoBehaviour
{
    [SerializeField] private RectTransform _shaker;
    [SerializeField] private float _shakeAngle = 10f;
    [SerializeField] private float _shakeSpeed = 2f;
    [SerializeField] private float _shakeDuration = 3f;
    [SerializeField] private EndingSwipe _swipeScript;


    private float _elapsed = 0f;
    private bool _shaking = true;
    private float _timePassed = 0f;

    private void Start()
    {
        Vibrate(2f);
        _shaking = true;
        if (_swipeScript != null)
            _swipeScript.enabled = false;
    }

    private void Update()
    {
        if (_shaker == null || !_shaking) return;

        _elapsed += Time.deltaTime * _shakeSpeed;
        _timePassed += Time.deltaTime;

        float angle = Mathf.Sin(_elapsed) * _shakeAngle;
        _shaker.localEulerAngles = new Vector3(0, 0, angle);
        if (_timePassed >= _shakeDuration)
        {
            _shaking = false;
            _shaker.localEulerAngles = Vector3.zero;

            if (_swipeScript != null)
                _swipeScript.enabled = true; 
        }

    }

    private void Vibrate(float durationInSeconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

                if (vibrator != null)
                {
                    long milliseconds = (long)(durationInSeconds * 1000);
                    vibrator.Call("vibrate", milliseconds);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Android 진동 실패: " + e.Message);
        }
#endif
    }
}
