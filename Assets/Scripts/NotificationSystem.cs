using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField] private GameObject notificationImageObject;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float slideInTime = 0.5f;
    [SerializeField] private float stayTime = 1.0f;
    
    // 알림 이미지 위치 관련
    private RectTransform imageRectTransform;
    [SerializeField] private Vector2 startPosition = new Vector2(-549f, 483f);
    [SerializeField] private Vector2 endPosition = new Vector2(-1f, 483f);
    
    // 플레이어 모델 참조
    [SerializeField] private PlayerModel playerModel;
    
    // 알림 관련 변수들
    private int _previousSubscribers;
    private int _previousStress;
    private int _previousFamous;
    
    // 구독자 마일스톤
    private readonly int[] subscriberMilestones = { 100, 1000, 10000, 100000, 1000000 };
    
    // 한 번만 표시할 알림 추적
    private HashSet<string> shownNotifications = new HashSet<string>();
    
    [System.Serializable]
    public class NotificationTextMapping
    {
        public string notificationType;
        public string message;
    }
    
    [SerializeField] private List<NotificationTextMapping> _notificationTexts;
    
    private void Awake()
    {
        // 알림 이미지 오브젝트 초기화
        if (notificationImageObject != null)
        {
            imageRectTransform = notificationImageObject.GetComponent<RectTransform>();
            // 초기에는 비활성화
            notificationImageObject.SetActive(false);
        }
        else
        {
            Debug.LogError("알림 이미지 오브젝트가 설정되지 않았습니다.");
        }
    }
    
    private void Start()
    {
        // 플레이어 모델 이벤트 구독
        if (playerModel != null)
        {
            playerModel.OnStressChanged += CheckStressNotification;
            playerModel.OnFamousChanged += CheckFamousNotification;
            playerModel.OnSubscriberChanged += CheckSubscriberNotification;
            
            // 초기값 저장
            _previousSubscribers = playerModel.Subscriber;
            _previousStress = playerModel.Stress;
            _previousFamous = playerModel.Famous;
        }
        else
        {
            Debug.LogError("PlayerModel 참조가 없습니다!");
        }
    }
    
    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (playerModel != null)
        {
            playerModel.OnStressChanged -= CheckStressNotification;
            playerModel.OnFamousChanged -= CheckFamousNotification;
            playerModel.OnSubscriberChanged -= CheckSubscriberNotification;
        }
    }
    
    // 구독자 수 변화 확인
    private void CheckSubscriberNotification(int newValue)
    {
        foreach (int milestone in subscriberMilestones)
        {
            if (_previousSubscribers < milestone && newValue >= milestone)
            {
                // 마일스톤 달성 알림
                string notificationType = "Subscribers" + milestone;
                ShowNotification(notificationType);
                break;
            }
        }
        
        _previousSubscribers = newValue;
    }
    
    // 스트레스 변화 확인
    private void CheckStressNotification(int newValue)
    {
        // 스트레스가 낮아진 경우
        if (_previousStress > 20 && newValue <= 20)
        {
            ShowNotification("StressLow");
        }
        // 스트레스가 위험 수준으로 높아진 경우
        else if (_previousStress < 80 && newValue >= 80)
        {
            ShowNotification("StressHigh");
        }
        
        _previousStress = newValue;
    }
    
    // 인기도 변화 확인
    private void CheckFamousNotification(int newValue)
    {
        // 인기도가 낮아진 경우
        if (_previousFamous > 20 && newValue <= 20)
        {
            ShowNotification("FamousLow");
        }
        // 인기도가 높아진 경우
        else if (_previousFamous < 80 && newValue >= 80)
        {
            ShowNotification("FamousHigh");
        }
        
        _previousFamous = newValue;
    }
    
    // 히든 선택지 발견 - 외부에서 호출
    public void NotifyHiddenChoiceFound(string choiceName)
    {
        // 이미 표시된 히든 선택지라면 다시 표시하지 않음
        string notificationKey = "Hidden_" + choiceName;
        if (shownNotifications.Contains(notificationKey))
            return;
            
        shownNotifications.Add(notificationKey);
        ShowNotification("HiddenChoiceFound");
    }
    
    // 알림 표시 메서드
    private void ShowNotification(string notificationType)
    {
        // 일회성 알림인 경우 처리
        if (notificationType.StartsWith("Subscribers") || notificationType == "HiddenChoiceFound")
        {
            if (shownNotifications.Contains(notificationType))
                return;
                
            shownNotifications.Add(notificationType);
        }
        
        // 알림 텍스트 설정
        string message = FindNotificationMessage(notificationType);
        if (!string.IsNullOrEmpty(message) && notificationImageObject != null)
        {
            // 텍스트 설정
            if (notificationText != null)
            {
                notificationText.text = message;
            }
            
            // 알림 활성화 (이것이 OnEnable을 트리거함)
            notificationImageObject.SetActive(true);
        }
    }
    
    // 알림 유형에 맞는 메시지 찾기
    private string FindNotificationMessage(string notificationType)
    {
        foreach (var mapping in _notificationTexts)
        {
            if (mapping.notificationType == notificationType)
                return mapping.message;
        }
        
        // 기본 메시지
        if (notificationType.StartsWith("Subscribers"))
        {
            string milestone = notificationType.Substring(11); // "Subscribers" 부분 제거
            return $"구독자 {milestone}명 달성!";
        }
        
        // 알림 타입에 따른 기본 메시지들
        switch (notificationType)
        {
            case "StressLow":
                return "스트레스 감소!";
            case "StressHigh":
                return "스트레스 위험!";
            case "FamousLow":
                return "인기도 하락!";
            case "FamousHigh":
                return "인기도 급상승!";
            case "HiddenChoiceFound":
                return "히든 선택지 발견!";
            default:
                return "알림!";
        }
    }
}