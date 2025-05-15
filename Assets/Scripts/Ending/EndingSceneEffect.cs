using UnityEngine;

public class EndingSceneEffect : MonoBehaviour
{
    [SerializeField] private RectTransform _shaker;
    [SerializeField] private float _shakeAngle = 10f;
    [SerializeField] private float _shakeSpeed = 2f;

    private float _elapsed = 0f;

    private void Start()
    {
        Vibrate(2f);
    }

    private void Update()
    {
        if (_shaker == null) return;

        _elapsed += Time.deltaTime * _shakeSpeed;

        float angle = Mathf.Sin(_elapsed) * _shakeAngle;
        _shaker.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Vibrate(float duration)
    {
        Invoke(nameof(StopVibrate), duration);
    }

    private void StopVibrate()
    {
        enabled = false;
        _shaker.localRotation = Quaternion.identity;
    }
}
