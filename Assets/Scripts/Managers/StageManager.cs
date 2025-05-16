using UnityEngine;

namespace Managers
{
    public class StageManager : MonoBehaviour
    {
        public static StageManager instance;

        [SerializeField] private StoryPresenter _storyPresenter;
        [SerializeField] private PlayerPresenter _playerPresenter;
        [SerializeField] private PlayerModel _playerModel;

        [SerializeField] private StorySelector _storySelector;

        private StoryData _curStoryData;
        public StoryData CurStoryData => _curStoryData;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            SetNewStory();
        }

        public void SetNewStory()
        {
            //Debug.LogError("NewStoryComing");
            _curStoryData = _storySelector.GetStory(_playerModel.Subscriber);
            _storyPresenter.SetNewStory(_curStoryData);

            ApplyTierBasedSubscriberGrowth();
        }

        private void ApplyTierBasedSubscriberGrowth()
        {
            int currentSubscribers = _playerModel.Subscriber;
            int tierBasedBonus = 0;
    
            // 현재 티어에 따라 다른 범위의 구독자 보너스 적용
            if (currentSubscribers <= 1000) // 티어 1
            {
                tierBasedBonus = Random.Range(50, 100);
            }
            else if (currentSubscribers <= 10000) // 티어 2
            {
                tierBasedBonus = Random.Range(20000, 30000);
            }
            else if (currentSubscribers <= 200000) // 티어 3
            {
                tierBasedBonus = Random.Range(30000, 50000);
            }
            else if (currentSubscribers <= 600000) // 티어 4
            {
                tierBasedBonus = Random.Range(45000, 80000);
            }
            else if (currentSubscribers <= 1500000) // 티어 5
            {
                tierBasedBonus = Random.Range(70000, 120000);
            }
            else if (currentSubscribers <= 5000000) // 티어 6
            {
                tierBasedBonus = Random.Range(100000, 450000);
            }
            else // 티어 7
            {
                tierBasedBonus = Random.Range(1000000, 1500000);
            }
            _playerModel.UpdatePlayerSubscriber(tierBasedBonus);
        }
    }
}