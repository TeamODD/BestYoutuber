using System.Collections.Generic;
using UnityEngine;

public class StorySelector : MonoBehaviour
{
    public enum StoryTierType
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Hidden_DisPatch,
        Hidden_LowStressandFamous,
        Hidden_LowStress,
        Hidden_HighStressandFamous,
        Hidden_StuckInTierOne  // 추가된 타입
    }
    
    [Header("Story Data Arrays")]
    [SerializeField] private StoryData[] _storyDatas1;
    [SerializeField] private StoryData[] _storyDatas2;
    [SerializeField] private StoryData[] _storyDatas3;
    [SerializeField] private StoryData[] _storyDatas4;
    [SerializeField] private StoryData[] _storyDatas5;
    [SerializeField] private StoryData[] _storyDatas6;
    [SerializeField] private StoryData[] _storyDatas7;
    [SerializeField] private StoryData[] _DisPatchDatas;
    [SerializeField] private StoryData[] _LowStressandFamousDatas;
    [SerializeField] private StoryData[] _LowStressDatas;
    [SerializeField] private StoryData[] _HighStressandFamousDatas;
    [SerializeField] private StoryData[] _StuckInTierOneDatas;  // 추가된 데이터 배열
    
    [Header("References")]
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private NotificationSystem _notificationSystem;

    private Dictionary<StoryTierType, Queue<StoryData>> _queueDictionary = new();
    private HashSet<StoryTierType> _discoveredHiddenStoryTypes = new();
    private bool _hiddenStoryActivated = false;
    private StoryTierType _activeHiddenStoryType;
    
    public event System.Action<StoryTierType> OnHiddenStoryActivated;
    
    private void Awake()
    {
        //InitializeQueues(); PopulateQueues();
        _queueDictionary.Add(StoryTierType.One, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Two, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Three, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Four, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Five, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Six, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Seven, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Hidden_DisPatch, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Hidden_LowStressandFamous, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Hidden_LowStress, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Hidden_HighStressandFamous, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Hidden_StuckInTierOne, new Queue<StoryData>());  // 추가된 큐
        
        foreach (var story in _storyDatas1) _queueDictionary[StoryTierType.One].Enqueue(story);
        foreach (var story in _storyDatas2) _queueDictionary[StoryTierType.Two].Enqueue(story);
        foreach (var story in _storyDatas3) _queueDictionary[StoryTierType.Three].Enqueue(story);
        foreach (var story in _storyDatas4) _queueDictionary[StoryTierType.Four].Enqueue(story);
        foreach (var story in _storyDatas5) _queueDictionary[StoryTierType.Five].Enqueue(story);
        foreach (var story in _storyDatas6) _queueDictionary[StoryTierType.Six].Enqueue(story);
        foreach (var story in _storyDatas7) _queueDictionary[StoryTierType.Seven].Enqueue(story);
        foreach (var story in _DisPatchDatas) _queueDictionary[StoryTierType.Hidden_DisPatch].Enqueue(story);
        foreach (var story in _LowStressandFamousDatas) _queueDictionary[StoryTierType.Hidden_LowStressandFamous].Enqueue(story);
        foreach (var story in _LowStressDatas) _queueDictionary[StoryTierType.Hidden_LowStress].Enqueue(story);
        foreach (var story in _HighStressandFamousDatas) _queueDictionary[StoryTierType.Hidden_HighStressandFamous].Enqueue(story);
        foreach (var story in _StuckInTierOneDatas) _queueDictionary[StoryTierType.Hidden_StuckInTierOne].Enqueue(story);  // 추가된 큐 초기화
    }
    
    private void Start()
    {
        if (_playerModel != null)
        {
            _playerModel.OnStressChanged += CheckHiddenStoryConditions;
            _playerModel.OnFamousChanged += CheckHiddenStoryConditions;
            _playerModel.OnSubscriberChanged += CheckHiddenStoryConditions;
        }
    }
    
    private void OnDestroy()
    {
        if (_playerModel != null)
        {
            _playerModel.OnStressChanged -= CheckHiddenStoryConditions;
            _playerModel.OnFamousChanged -= CheckHiddenStoryConditions;
            _playerModel.OnSubscriberChanged -= CheckHiddenStoryConditions;
        }
    }
    
    public StoryData GetStory(int subscriber)
    {
        if (_hiddenStoryActivated)
        {
            if (_queueDictionary[_activeHiddenStoryType].Count > 0)
            {
                StoryData hiddenStory = _queueDictionary[_activeHiddenStoryType].Dequeue();
                
                if (_queueDictionary[_activeHiddenStoryType].Count == 0)
                {
                    _hiddenStoryActivated = false;
                }
                
                return hiddenStory;
            }
            else
            {
                _hiddenStoryActivated = false;
            }
        }
        
        StoryTierType curTier = GetStoryTierType(subscriber);

        if (_queueDictionary.TryGetValue(curTier, out var storys) && storys.Count > 0)
        {
            return storys.Dequeue();
        }
        else
        {
            Debug.LogError("Story queue is empty: " + curTier);
            return null;
        }
    }
    
    private void CheckHiddenStoryConditions(int _)
    {
        if (_hiddenStoryActivated) return;
        
        if (_playerModel.Stress <= 20 && _playerModel.Famous <= 20)
        {
            ActivateHiddenStory(StoryTierType.Hidden_LowStressandFamous);
        }
        else if (_playerModel.Stress <= 20)
        {
            ActivateHiddenStory(StoryTierType.Hidden_LowStress);
        }
        else if (_playerModel.Stress >= 80 && _playerModel.Famous >= 80)
        {
            ActivateHiddenStory(StoryTierType.Hidden_HighStressandFamous);
        }
        else if (_playerModel.Subscriber <= 1000 && 
                _queueDictionary[StoryTierType.One].Count == 0)
        {
            ActivateHiddenStory(StoryTierType.Hidden_StuckInTierOne);
        }
        else if (_playerModel.Stress >= 80 && _playerModel.Famous <= 20)
        {
            ActivateHiddenStory(StoryTierType.Hidden_DisPatch);
        }
    }
    
    private void ActivateHiddenStory(StoryTierType hiddenType)
    {
        if (IsHiddenStoryType(hiddenType) && _discoveredHiddenStoryTypes.Contains(hiddenType))
        {
            Debug.Log($"Already discovered hidden story: {hiddenType}");
            return;
        }
        if (_queueDictionary[hiddenType].Count > 0)
        {
            _hiddenStoryActivated = true;
            _activeHiddenStoryType = hiddenType;

            if (IsHiddenStoryType(hiddenType))
            {
                _discoveredHiddenStoryTypes.Add(hiddenType);
            }
            
            OnHiddenStoryActivated?.Invoke(hiddenType);
            
            if (_notificationSystem != null)
            {
                string hiddenTypeName = hiddenType.ToString().Substring(7);
                _notificationSystem.NotifyHiddenChoiceFound(hiddenTypeName);
            }
            Debug.Log($"Hidden story activated: {hiddenType}");
        }
    }
    
    private bool IsHiddenStoryType(StoryTierType type)
    {
        // 열거형 이름에 "Hidden_"이 포함되어 있으면 히든 타입으로 간주
        return type.ToString().StartsWith("Hidden_");
    }
    
    private string GetHiddenStoryNotificationMessage(StoryTierType hiddenType)
    {
        switch (hiddenType)
        {
            case StoryTierType.Hidden_LowStressandFamous:
                return "히든 선택지 발견: 무명의 평화!";
            case StoryTierType.Hidden_LowStress:
                return "히든 선택지 발견: 평화로운 일상!";
            case StoryTierType.Hidden_HighStressandFamous:
                return "히든 선택지 발견: 스트레스 많은 스타!";
            case StoryTierType.Hidden_StuckInTierOne:
                return "히든 선택지 발견: 침체된 채널!";
            case StoryTierType.Hidden_DisPatch:
                return "히든 선택지 발견: 디스패치!";
            default:
                return "히든 선택지 발견!";
        }
    }

    private StoryTierType GetStoryTierType(int subscriber)
    {
        if (subscriber <= 1000)
            return StoryTierType.One;
        else if (subscriber <= 10000)
            return StoryTierType.Two;
        else if (subscriber <= 200000)
            return StoryTierType.Three;
        else if (subscriber <= 600000)
            return StoryTierType.Four;
        else if (subscriber <= 1500000)
            return StoryTierType.Five;
        else if (subscriber <= 5000000)
            return StoryTierType.Six;
        else
            return StoryTierType.Seven;
    }
    
    public void ForceActivateHiddenStory(string hiddenTypeName)
    {
        if (System.Enum.TryParse<StoryTierType>(hiddenTypeName, out var hiddenType))
        {
            ActivateHiddenStory(hiddenType);
        }
        else
        {
            Debug.LogError($"Invalid hidden story type: {hiddenTypeName}");
        }
    }
    
    public string GetActiveHiddenStoryType()
    {
        return _hiddenStoryActivated ? _activeHiddenStoryType.ToString() : "None";
    }
    
    public void ResetDiscoveredHiddenStories()
    {
        _discoveredHiddenStoryTypes.Clear();
        Debug.Log("All hidden story discovery records have been reset.");
    }
    
    public bool IsHiddenStoryDiscovered(StoryTierType hiddenType)
    {
        return _discoveredHiddenStoryTypes.Contains(hiddenType);
    }
}