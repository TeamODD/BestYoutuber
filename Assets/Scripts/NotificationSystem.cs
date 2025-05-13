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
    
    private RectTransform imageRectTransform;
    [SerializeField] private Vector2 startPosition = new Vector2(-549f, 483f);
    [SerializeField] private Vector2 endPosition = new Vector2(-1f, 483f);
    
    [SerializeField] private PlayerModel playerModel;
    
    private int _previousSubscribers;
    private int _previousStress;
    private int _previousFamous;
    
    private readonly int[] _subscriberMilestones = { 100, 1000, 10000, 100000, 1000000 };
    
    private HashSet<string> _shownNotifications = new HashSet<string>();
    
    [System.Serializable]
    public class NotificationTextMapping
    {
        public string notificationType;
        public string message;
    }
    
    [SerializeField] private List<NotificationTextMapping> _notificationTexts;
    
    private void Awake()
    {
        if (notificationImageObject != null)
        {
            imageRectTransform = notificationImageObject.GetComponent<RectTransform>();
            notificationImageObject.SetActive(false);
        }
    }
    
    private void Start()
    {
        if (playerModel != null)
        {
            playerModel.OnStressChanged += CheckStressNotification;
            playerModel.OnFamousChanged += CheckFamousNotification;
            playerModel.OnSubscriberChanged += CheckSubscriberNotification;
            
            _previousSubscribers = playerModel.Subscriber;
            _previousStress = playerModel.Stress;
            _previousFamous = playerModel.Famous;
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
    
    private void CheckSubscriberNotification(int newValue)
    {
        foreach (int milestone in _subscriberMilestones)
        {
            if (_previousSubscribers < milestone && newValue >= milestone)
            {
                string notificationType = "Subscribers" + milestone;
                ShowNotification(notificationType);
                break;
            }
        }
        _previousSubscribers = newValue;
    }
    
    private void CheckStressNotification(int newValue)
    {
        if (_previousStress > 20 && newValue <= 20)
        {
            ShowNotification("StressLow");
        }
        else if (_previousStress < 80 && newValue >= 80)
        {
            ShowNotification("StressHigh");
        }
        _previousStress = newValue;
    }
    
    private void CheckFamousNotification(int newValue)
    {
        if (_previousFamous > 20 && newValue <= 20)
        {
            ShowNotification("FamousLow");
        }
        else if (_previousFamous < 80 && newValue >= 80)
        {
            ShowNotification("FamousHigh");
        }
        
        _previousFamous = newValue;
    }
    
    public void NotifyHiddenChoiceFound(string choiceName)
    {
        string notificationKey = "Hidden_" + choiceName;
        if (_shownNotifications.Contains(notificationKey))
            return;
            
        _shownNotifications.Add(notificationKey);
        ShowNotification("HiddenChoiceFound");
    }
    
    public void ShowNotification(string notificationType)
    {
        // 일회성 알림인 경우 처리
        if (notificationType.StartsWith("Subscribers") || notificationType == "HiddenChoiceFound")
        {
            if (_shownNotifications.Contains(notificationType))
                return;
                
            _shownNotifications.Add(notificationType);
        }
        
        string message = FindNotificationMessage(notificationType);
        if (!string.IsNullOrEmpty(message) && notificationImageObject != null)
        {
            // 텍스트 설정
            if (notificationText != null)
            {
                notificationText.text = message;
            }
            
            notificationImageObject.SetActive(true);
        }
    }
    
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