using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject notificationImageObject;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private PlayerModel playerModel;

    [Header("Animation Settings")]
    [SerializeField] private float slideInTime = 0.5f;
    [SerializeField] private float stayTime = 1.0f;

    [Header("Player State Tracking")]
    private int _previousSubscribers;
    private int _previousStress;
    private int _previousFamous;
    
    [Header("Milestone Settings")]
    [SerializeField] private int[] _subscriberMilestones = { 100, 1000, 10000, 100000, 1000000 };
    
    public enum NotificationType
    {
        OneTime,    
        Repeatable  
    }

    private HashSet<string> _shownNotifications = new();
    
    private Queue<string> _notificationQueue = new();
    private bool _isProcessingQueue = false;
    private bool _isNotificationActive = false;
    
    [System.Serializable]
    public class NotificationTextMapping
    {
        [Tooltip("알림 타입 (예: Subscribers100, StressLow, Hidden_DisPatch)")]
        public string notificationType;
        
        [Tooltip("알림 발생 유형 (일회용/반복)")]
        public NotificationType type = NotificationType.Repeatable;
        
        [Tooltip("표시할 메시지")]
        [TextArea(1, 3)]
        public string message;
    }
    
    [Header("Custom Notification Messages")]
    [SerializeField] private List<NotificationTextMapping> _notificationTexts;
    
    private HashSet<string> _predefinedOneTimeNotifications = new HashSet<string>
    {
        "SilverButton", 
        "GoldButton",
        "Subscribers100",
        "Subscribers1000",
        "Subscribers10000",
        "Subscribers100000",
        "Subscribers1000000"
    };
    
    private void Awake()
    {
        if (notificationImageObject != null)
        {
            // NotificationImage 설정 (존재한다면)
            NotificationImage notificationImage = notificationImageObject.GetComponent<NotificationImage>();
            if (notificationImage != null)
            {
                notificationImage.SetAnimationTiming(slideInTime, stayTime);
                // 알림이 닫힐 때 호출될 이벤트 연결
                //notificationImage.OnNotificationClosed += OnNotificationClosed;
            }
            
            notificationImageObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Notification image object reference is missing!");
        }
    }
    
    private void Start()
    {
        if (playerModel != null)
        {
            playerModel.OnStressChanged += CheckStressNotification;
            playerModel.OnFamousChanged += CheckFamousNotification;
            playerModel.OnSubscriberChanged += CheckSubscriberNotification;
            
            // 초기값 장
            _previousSubscribers = playerModel.Subscriber;
            _previousStress = playerModel.Stress;
            _previousFamous = playerModel.Famous;
        }
        else
        {
            Debug.LogWarning("Player model reference is missing!");
        }
    }
    
    private void OnDestroy()
    {
        if (playerModel != null)
        {
            playerModel.OnStressChanged -= CheckStressNotification;
            playerModel.OnFamousChanged -= CheckFamousNotification;
            playerModel.OnSubscriberChanged -= CheckSubscriberNotification;
        }
        
        if (notificationImageObject != null)
        {
            NotificationImage notificationImage = notificationImageObject.GetComponent<NotificationImage>();
            if (notificationImage != null)
            {
                //notificationImage.OnNotificationClosed -= OnNotificationClosed;
            }
        }
    }
    
    private void OnNotificationClosed()
    {
        _isNotificationActive = false;
        
        if (_notificationQueue.Count > 0)
        {
            ProcessNextNotification();
        }
    }
    
    private void CheckSubscriberNotification(int newValue)
    {
        foreach (int milestone in _subscriberMilestones)
        {
            if (_previousSubscribers < milestone && newValue >= milestone)
            {
                // 마일스톤 달성 알림
                string notificationType = "Subscribers" + milestone;
                QueueNotification(notificationType);
                break;
            }
        }
        
        if (newValue > _previousSubscribers * 1.5f && _previousSubscribers > 0)  
        {
            QueueNotification("SubscribersIncrease");
        }
        else if (newValue < _previousSubscribers * 0.7f && _previousSubscribers > 100)  
        {
            QueueNotification("SubscribersDecrease");
        }
        
        _previousSubscribers = newValue;
    }
    
    // 스트레스 변화 확인
    private void CheckStressNotification(int newValue)
    {
        // 스트레스 변화 임계점
        const int lowThreshold = 20;
        const int highThreshold = 80;
        
        // 스트레스 감소 (20 이하)
        if (_previousStress > lowThreshold && newValue <= lowThreshold)
        {
            QueueNotification("StressLow");
        }
        // 스트레스 위험 (80 이상)
        else if (_previousStress < highThreshold && newValue >= highThreshold)
        {
            QueueNotification("StressHigh");
        }
        // 스트레스가 경계를 벗어날 때 알림 기록 삭제 (반복 알림 가능하게)
        else if ((newValue > lowThreshold && _previousStress <= lowThreshold) || 
                 (newValue < highThreshold && _previousStress >= highThreshold))
        {
            // 반복 알림을 위해 알림 기록에서 제거
            ClearNotificationFromHistory("StressLow");
            ClearNotificationFromHistory("StressHigh");
        }
        
        _previousStress = newValue;
    }
    
    // 인기도 변화 확인
    private void CheckFamousNotification(int newValue)
    {
        // 인기도 변화 임계점
        const int lowThreshold = 20;
        const int highThreshold = 80;
        
        // 인기도 하락 (20 이하)
        if (_previousFamous > lowThreshold && newValue <= lowThreshold)
        {
            QueueNotification("FamousLow");
        }
        // 인기도 상승 (80 이상)
        else if (_previousFamous < highThreshold && newValue >= highThreshold)
        {
            QueueNotification("FamousHigh");
        }
        // 인기도가 경계를 벗어날 때 알림 기록 삭제 (반복 알림 가능하게)
        else if ((newValue > lowThreshold && _previousFamous <= lowThreshold) || 
                 (newValue < highThreshold && _previousFamous >= highThreshold))
        {
            // 반복 알림을 위해 알림 기록에서 제거
            ClearNotificationFromHistory("FamousLow");
            ClearNotificationFromHistory("FamousHigh");
        }
        
        _previousFamous = newValue;
    }
    
    // 반복 알림을 위해 히스토리에서 특정 알림 제거
    private void ClearNotificationFromHistory(string notificationType)
    {
        if (!IsOneTimeNotification(notificationType))
        {
            _shownNotifications.Remove(notificationType);
        }
    }
    
    // 히든 선택지 발견 알림 - 외부에서 호출
    public void NotifyHiddenChoiceFound(string hiddenTypeName)
    {
        string notificationType = "Hidden_" + hiddenTypeName;
        QueueNotification(notificationType);
    }
    
    // 특정 버튼 획득 알림 - 외부에서 호출
    public void NotifyButtonAchieved(string buttonType)
    {
        QueueNotification(buttonType + "Button");
    }
    
    // 알림 큐에 추가
    private void QueueNotification(string notificationType)
    {
        // 일회성 알림인 경우 이미 표시되었는지 확인
        if (IsOneTimeNotification(notificationType))
        {
            if (_shownNotifications.Contains(notificationType))
                return;
                
            _shownNotifications.Add(notificationType);
        }
        
        // 큐에 추가
        _notificationQueue.Enqueue(notificationType);
        
        // 알림이 활성화되어 있지 않은 경우, 큐 처리 시작
        if (!_isNotificationActive)
        {
            ProcessNextNotification();
        }
    }
    
    // 큐에서 다음 알림 처리
    private void ProcessNextNotification()
    {
        if (_notificationQueue.Count > 0)
        {
            string notificationType = _notificationQueue.Dequeue();
            ShowNotification(notificationType);
        }
    }
    
    // 일회성 알림 확인
    private bool IsOneTimeNotification(string notificationType)
    {
        // 미리 정의된 일회용 알림 확인
        if (_predefinedOneTimeNotifications.Contains(notificationType))
            return true;
            
        // 히든 선택지는 모두 일회용
        if (notificationType.StartsWith("Hidden_"))
            return true;
            
        // 구독자 마일스톤은 일회용 (SubscribersIncrease/Decrease는 제외)
        if (notificationType.StartsWith("Subscribers") && 
            !notificationType.Equals("SubscribersIncrease") && 
            !notificationType.Equals("SubscribersDecrease"))
            return true;
            
        // 커스텀 매핑 확인
        foreach (var mapping in _notificationTexts)
        {
            if (mapping.notificationType == notificationType)
                return mapping.type == NotificationType.OneTime;
        }
        
        // 기본값은 반복 알림
        return false;
    }
    
    // public void NotifyButtonAchieved(string buttonType)
    // {
    //     if (_notificationSystem != null)
    //     {
    //         _notificationSystem.NotifyButtonAchieved(buttonType);
    //     }
    // }
    
    // 알림 표시 - 외부에서 호출 가능
    public void ShowNotification(string notificationType)
    {
        // 메시지 찾기
        string message = FindNotificationMessage(notificationType);
        if (!string.IsNullOrEmpty(message) && notificationImageObject != null)
        {
            // 텍스트 설정
            if (notificationText != null)
            {
                notificationText.text = message;
            }
            
            // 알림 활성화 상태 설정
            _isNotificationActive = true;
            
            // 알림 표시
            notificationImageObject.SetActive(true);
        }
    }
    
    // 알림 메시지 찾기
    private string FindNotificationMessage(string notificationType)
    {
        // 사용자 정의 매핑에서 먼저 찾기
        foreach (var mapping in _notificationTexts)
        {
            if (mapping.notificationType == notificationType)
                return mapping.message;
        }
        
        // 구독자 마일스톤 알림
        if (notificationType.StartsWith("Subscribers") && !notificationType.Equals("SubscribersIncrease") && !notificationType.Equals("SubscribersDecrease"))
        {
            string milestone = notificationType.Substring(11); // "Subscribers" 부분 제거
            return $"구독자 {milestone}명 달성!";
        }
        
        // 히든 스토리 알림
        if (notificationType.StartsWith("Hidden_"))
        {
            // "Hidden_" 제거 후 남은 부분 추출
            string hiddenTypeStr = notificationType.Substring(7);
            
            switch (hiddenTypeStr)
            {
                case "LowStressandFamous":
                    return "히든 선택지 발견: 무명의 평화!";
                case "LowStress":
                    return "히든 선택지 발견: 평화로운 일상!";
                case "HighStressandFamous":
                    return "히든 선택지 발견: 스트레스 많은 스타!";
                case "StuckInTierOne":
                    return "히든 선택지 발견: 침체된 채널!";
                case "DisPatch":
                    return "히든 선택지 발견: 디스패치!";
                default:
                    return "히든 선택지 발견!";
            }
        }
        
        // 기본 알림 유형
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
            case "SubscribersIncrease":
                return "구독자 급상승!";
            case "SubscribersDecrease":
                return "구독자 급하락!";
            case "SilverButton":
                return "실버 버튼을 받았습니다!!";
            case "GoldButton":
                return "골드 버튼을 받았습니다!!";
            default:
                return "알림!";
        }
    }
    
    // 강제로 현재 알림 닫기 (외부에서 호출 가능)
    public void CloseCurrentNotification()
    {
        if (_isNotificationActive && notificationImageObject != null)
        {
            notificationImageObject.SetActive(false);
            _isNotificationActive = false;
            
            // 다음 알림 처리
            if (_notificationQueue.Count > 0)
            {
                // 약간의 지연 후 다음 알림 표시
                StartCoroutine(DelayedProcessNextNotification(0.1f));
            }
        }
    }
    
    // 약간의 지연 후 다음 알림 처리
    private IEnumerator DelayedProcessNextNotification(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProcessNextNotification();
    }
    
    // 모든 알림 기록 초기화 (테스트용)
    public void ResetNotificationHistory()
    {
        _shownNotifications.Clear();
        Debug.Log("Notification history has been reset");
    }
    
    // 특정 알림이 이미 표시되었는지 확인 (디버깅용)
    public bool HasNotificationBeenShown(string notificationType)
    {
        return _shownNotifications.Contains(notificationType);
    }
    
    // 특정 알림 타입이 일회용인지 확인 (디버깅용)
    public bool IsNotificationOneTime(string notificationType)
    {
        return IsOneTimeNotification(notificationType);
    }
    
    // 현재 큐에 있는 알림 수 확인 (디버깅용)
    public int GetQueueCount()
    {
        return _notificationQueue.Count;
    }
}