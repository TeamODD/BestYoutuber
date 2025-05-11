using System.Collections.Generic;
using System.ComponentModel;
using Stage;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("Subscriber Story Data")]
    [SerializeField] private SubscriberTierGroup[] _subscriberTierGroups;
    
    [Header("Special Milestone Story Data")] 
    public SpecialStory[] _specialStories;

    //[SerializeField] private StoryData[] _storyDatas;
    [SerializeField] private StoryPresenter _storyPresenter;
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private PlayerModel _playerModel;
    
    [Header("디버그 정보")]
    [SerializeField] private string _currentTierName = "";
    [SerializeField] private int _currentSubscribers = 0;
    [SerializeField] private int _debugAddSubscribers = 100; // 한 번에 추가할 구독자 수

    private StoryData _curStoryData;

    private int _currentTierIndex = 0;
    private int _currentStoryIndex = 0;

    private HashSet<int> _seenSpecialStories = new();

    private bool _isInSpecialStory = false;
    private int _currentSpecialStoryIndex = -1;
    public StoryData CurStoryData => _curStoryData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        _currentTierIndex = 0;
        _currentStoryIndex = 0;
        UpdateCurrentStory();
    }

    public void SetNewStory()
    {
        if (_isInSpecialStory)
        {
            _isInSpecialStory = false;
            _currentSpecialStoryIndex = -1;
            var currentTierGroup = _subscriberTierGroups[_currentTierIndex];

            _currentStoryIndex++;

            if (_currentStoryIndex >= currentTierGroup.stories.Length)
            {
                Debug.Log($"{currentTierGroup.tierName} All subScribers check!");

                _currentStoryIndex = currentTierGroup.stories.Length - 1;
            }
        }
        else
        {
            var currentTierGroup = _subscriberTierGroups[_currentTierIndex];
            _currentStoryIndex++;
            if (_currentStoryIndex >= currentTierGroup.stories.Length)
            {
                Debug.Log($"{currentTierGroup.tierName} All subScribers check!");

                _currentStoryIndex = currentTierGroup.stories.Length - 1;
            }
        }

        UpdateCurrentStory();
    }

    private void UpdateCurrentStory()
    {
        if (_isInSpecialStory)
            return;
        var _currentSubscribers = _playerModel.Subscriber;

        int newTierIndex = GetTierIndexForSubscriberCount(_currentSubscribers);

        if (newTierIndex != _currentTierIndex)
        {
            _currentTierIndex = newTierIndex;
            _currentStoryIndex = 0;
        }

        var currentTierGroup = _subscriberTierGroups[_currentTierIndex];

        if (_currentStoryIndex < currentTierGroup.stories.Length)
        {
            _curStoryData = currentTierGroup.stories[_currentStoryIndex];
            _storyPresenter.SetNewStory(_curStoryData);
        }
        
        Debug.Log("Update!!");
    }

    private int GetTierIndexForSubscriberCount(int subscribers)
    {
        for (var i = 0; i < _subscriberTierGroups.Length; i++)
        {
            var tier = _subscriberTierGroups[i];
            if (subscribers >= tier.minSubscribers && subscribers <= tier.maxSubscribers)
            {
                return i;
                Debug.Log("Next Tier");
            }
        }

        return _subscriberTierGroups.Length - 1;
    }

    public void OnSubscriberCountChanged(int newSubscriberCount)
    {
        if (CheckForSpecialStory(newSubscriberCount))
        {
            return; // 특별 스토리가 있으면 일반 진행 중단
        }
            
        int oldTierIndex = _currentTierIndex;
        int newTierIndex = GetTierIndexForSubscriberCount(newSubscriberCount);
            
        if (oldTierIndex != newTierIndex)
        {
            Debug.Log($"티어 변경! {_subscriberTierGroups[oldTierIndex].tierName} -> {_subscriberTierGroups[newTierIndex].tierName}");
        }
    }
    private void ShowTierTransition(int fromTier, int toTier)
    {
        string message = toTier > fromTier
            ? $"Congratuation! {_subscriberTierGroups[toTier].tierName} Tier!"
            : $"{_subscriberTierGroups[toTier].tierName} ";
    }
    public void DebugForceTierChange()
    {
        if (_currentTierIndex < _subscriberTierGroups.Length - 1)
        {
            _currentTierIndex++;
            _currentStoryIndex = 0;
            UpdateCurrentStory();
        }
    }
    
    private bool CheckForSpecialStory(int subscriberCount)
    {
        foreach (var specialStory in _specialStories)
        {
            if (_seenSpecialStories.Contains(specialStory.milestoneSubscriberCount))
                continue;
                
            if (subscriberCount >= specialStory.milestoneSubscriberCount)
            {
                Debug.Log($"특별 스토리 발동! {specialStory.milestoneSubscriberCount:N0}명 마일스톤");
                    
                // 특별 스토리 표시하고 진행
                _seenSpecialStories.Add(specialStory.milestoneSubscriberCount);
                _isInSpecialStory = true;
                _currentSpecialStoryIndex = System.Array.IndexOf(_specialStories, specialStory);
                    
                // 특별 스토리로 전환
                _curStoryData = specialStory.specialStoryData;
                _storyPresenter.SetNewStory(_curStoryData);
                    
                return true; // 특별 스토리 진행 중
            }
        }
            
        return false; // 특별 스토리 없음
    }
}