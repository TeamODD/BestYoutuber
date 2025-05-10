
using UnityEngine;

[System.Serializable]
public class SubscriberTierGroup
{
    [Header("Subscriber Tier Group")] public string tierName;
    public int minSubscribers;
    public int maxSubscribers;
    [Header("Story Tier Data")] public StoryData[] stories; 
}
