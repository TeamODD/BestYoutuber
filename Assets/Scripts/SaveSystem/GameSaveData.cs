using System.Collections.Generic;

[System.Serializable]
public class GameSaveData
{
    // 플레이어 기본 데이터
    public int subscriber;
    public int stress;
    public int famous;
    
    public List<string> discoveredHiddenStories = new List<string>();
    
    public List<string> achievedMilestones = new List<string>();
    
    public int currentStoryTier;
    public int storyProgress;
    
    public List<string> shownNotifications = new List<string>();
    
    public bool silverButtonAchieved;
    public bool goldButtonAchieved;
    
    public long saveTimestamp;
}