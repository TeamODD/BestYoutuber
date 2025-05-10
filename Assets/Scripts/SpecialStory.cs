

using UnityEngine;

[System.Serializable]
    public class SpecialStory
    {
        [Header("MileStone Information")] public string specialStoryName;
        public int milestoneSubscriberCount;
        [Header("Special Story Data")]
        public StoryData specialStoryData;

        [Header("Special Effect")] public AudioClip milestoneSound;
        public GameObject specialEffect; //특별 파티클 
        
        //추가 보너스 점수를 넣을 수 있나? System.Serializable로 만들고 StatsChange로 변경하면 될것같기도하고...) 
    }
