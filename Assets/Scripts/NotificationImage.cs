using System.Collections;
using UnityEngine;

public class NotificationImage : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Vector2 startPosition = new Vector2(-549f, 483f);
    [SerializeField] private Vector2 endPosition = new Vector2(-1f, 483f);
    [SerializeField] private float slideInTime = 0.5f;
    [SerializeField] private float stayTime = 1.0f;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    private void OnEnable()
    {
        // 알림이 활성화될 때 애니메이션 시작
        StartCoroutine(AnimateNotification());
    }
    
    private IEnumerator AnimateNotification()
    {
        // 시작 위치 설정
        rectTransform.anchoredPosition = startPosition;
        
        // 1. 슬라이드 인 애니메이션
        float elapsed = 0;
        while (elapsed < slideInTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideInTime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
        
        // 종료 위치에 정확히 배치
        rectTransform.anchoredPosition = endPosition;
        
        // 2. 화면에 머무르는 시간
        yield return new WaitForSeconds(stayTime);
        
        // 3. 알림 비활성화
        gameObject.SetActive(false);
    }
}