using UnityEngine;
using System;

public class EndingSceneEffect : MonoBehaviour
{
    [SerializeField] private RectTransform _shaker;
    [SerializeField] private float _shakeAngle = 10f;
    [SerializeField] private float _shakeSpeed = 10f;
    [SerializeField] private float _shakeDuration = 1f;

    private float _elapsed = 0f;
    private bool _shaking = true;

    public event Action OnShakeComplete; 

    private void Start()
    {
        Vibrate(_shakeDuration);
    }

    private void Update()
    {
        if (!_shaking || _shaker == null) return;

        _elapsed += Time.deltaTime;

        float angle = Mathf.Sin(_elapsed * _shakeSpeed) * _shakeAngle;
        _shaker.localEulerAngles = new Vector3(0, 0, angle);

        if (_elapsed >= _shakeDuration)
        {
            _shaking = false;
            _shaker.localEulerAngles = Vector3.zero;

            Debug.Log("흔들림 완료");
            OnShakeComplete?.Invoke(); 
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
            Debug.LogWarning("진동 실패: " + e.Message);
        }
#endif
    }
}
