using System.Collections;
using UnityEngine;

public class NotificationImage : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    [Header("Position Settings")]
    [SerializeField] private Vector2 startPosition = new Vector2(-549f, 483f);
    [SerializeField] private Vector2 endPosition = new Vector2(-1f, 483f);
    
    [Header("Timing Settings")]
    [SerializeField] private float slideInTime = 0.5f;
    [SerializeField] private float stayTime = 1.0f;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetAnimationTiming(float inTime, float waitTime)
    {
        slideInTime = inTime;
        stayTime = waitTime;
    }
    
    private void OnEnable()
    {
        StartCoroutine(AnimateNotification());
    }
    
    private IEnumerator AnimateNotification()
    {
        _rectTransform.anchoredPosition = startPosition;
        
        float elapsed = 0;
        while (elapsed < slideInTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideInTime;
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
        
        _rectTransform.anchoredPosition = endPosition;
        
        yield return new WaitForSeconds(stayTime);
        
        gameObject.SetActive(false);
    }
}