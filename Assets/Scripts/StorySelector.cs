using System.Collections.Generic;
using UnityEngine;

public class StorySelector : MonoBehaviour
{
    private enum StoryTierType
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven
    }

    [SerializeField] private StoryData[] _storyDatas1;
    [SerializeField] private StoryData[] _storyDatas2;
    [SerializeField] private StoryData[] _storyDatas3;
    [SerializeField] private StoryData[] _storyDatas4;
    [SerializeField] private StoryData[] _storyDatas5;
    [SerializeField] private StoryData[] _storyDatas6;
    [SerializeField] private StoryData[] _storyDatas7;

    private Dictionary<StoryTierType, Queue<StoryData>> _queueDictionary = new();

    private void Awake()
    {
        _queueDictionary.Add(StoryTierType.One, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Two, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Three, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Four, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Five, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Six, new Queue<StoryData>());
        _queueDictionary.Add(StoryTierType.Seven, new Queue<StoryData>());
        foreach (var story in _storyDatas1) _queueDictionary[StoryTierType.One].Enqueue(story);
        foreach (var story in _storyDatas2) _queueDictionary[StoryTierType.Two].Enqueue(story);
        foreach (var story in _storyDatas3) _queueDictionary[StoryTierType.Three].Enqueue(story);
        foreach (var story in _storyDatas4) _queueDictionary[StoryTierType.Four].Enqueue(story);
        foreach (var story in _storyDatas5) _queueDictionary[StoryTierType.Five].Enqueue(story);
        foreach (var story in _storyDatas6) _queueDictionary[StoryTierType.Six].Enqueue(story);
        foreach (var story in _storyDatas7) _queueDictionary[StoryTierType.Seven].Enqueue(story);
    }
    
    public StoryData GetStory(int subscriber)
    {
        StoryTierType curTier = GetStoryTierType(subscriber);

        if (_queueDictionary.TryGetValue(curTier, out var storys))
        {
            return storys.Dequeue();
        }
        else
        {
            Debug.LogError("EEEEEEEEEEEEEEEEEE");
            return null;
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
}