using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class StoryPresenter : MonoBehaviour
    {
        [SerializeField] private ChoiceView _choiceView;
        [SerializeField] private ChoiceSelectView _choiceSelectView;
        [SerializeField] private ChoiceResultView _choiceResultView;
        [SerializeField] private StoryModel _storyModel;
        [SerializeField] private FingerDetector _fingerDetector;
    
        // [Header("특별 효과 UI")]
        // [SerializeField] private GameObject _milestoneEffectPrefab;
        // [SerializeField] private Transform _effectContainer;
        // [SerializeField] private AudioSource _audioSource;

        private StoryData _currentStoryData;

        public void SetNewStory(StoryData story)
        {
            _currentStoryData = story;
            var isSpecialStory = CheckIfSpecialStory(story);

            if (isSpecialStory)
            {
                //ShowSpecialStoryEffects();
            }
        
            _fingerDetector.canMove = true;

            _choiceView.SetTmpText((int)ChoiceView.Tmps.ChoiceText, story.ChoiceText);
            _choiceView.SetImageSprite((int)ChoiceView.Images.ChoiceImage, story.ChoiceSprite);
            _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectText, story.LeftCommentData.ChoiceComment);
            _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectText, story.RightCommentData.ChoiceComment);

            Image leftImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.LeftSelectChanceImage);
            leftImage.fillAmount = story.LeftCommentData.SuccessChance / 100.0f;
            _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectChanceText, story.LeftCommentData.SuccessChance.ToString("f0"));

            Image rightImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.RightSelectChanceImage);
            rightImage.fillAmount = story.RightCommentData.SuccessChance / 100.0f;
            _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectChanceText, story.RightCommentData.SuccessChance.ToString("f0"));

            _storyModel.SetLeftSuccess(story.LeftCommentData.SuccessChance);
            _storyModel.SetRightSuccess(story.RightCommentData.SuccessChance);

            SetupResults(story);
        
        }
        private bool CheckIfSpecialStory(StoryData story)
        {
            // StageManager에서 특별 스토리 목록과 비교
            var stageManager = StageManager.instance;
            foreach (var specialStory in stageManager._specialStories)
            {
                if (specialStory.specialStoryData == story)
                {
                    ApplySpecialStoryBonus(specialStory);
                    return true;
                }
            }
            return false;
        }
        
        // private void ShowSpecialStoryEffects()
        // {
        //     // 특별 효과 애니메이션
        //     if (_milestoneEffectPrefab != null)
        //     {
        //         GameObject effect = Instantiate(_milestoneEffectPrefab, _effectContainer);
        //         Destroy(effect, 3f); // 3초 후 제거
        //     }
        //     
        //     Debug.Log("✨ 특별 스토리 시작! ✨");
        // }
        
        private void ApplySpecialStoryBonus(SpecialStory specialStory)
        {
            // if (specialStory.milestoneSound != null && _audioSource != null)
            // {
            //     _audioSource.PlayOneShot(specialStory.milestoneSound);
            // }
            //
            // if (specialStory.specialEffect != null)
            // {
            //     GameObject effect = Instantiate(specialStory.specialEffect, _effectContainer);
            //     Destroy(effect, 5f);
            // }
            
            // // 진입 보너스 적용
            // if (specialStory.entryBonus != null)
            // {
            //     var playerModel = FindObjectOfType<Player.PlayerModel>();
            //     if (playerModel != null)
            //     {
            //         playerModel.UpdatePlayerStress(specialStory.entryBonus.stressChange);
            //         playerModel.UpdatePlayerFamous(specialStory.entryBonus.fameChange);
            //         playerModel.UpdatePlayerSubscriber(specialStory.entryBonus.subscriberChange);
            //     }
            // }
        }
    
        private void SetupResults(StoryData story)
        {
            // 왼쪽 결과
            if (_storyModel.LeftSuccess)
            {
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText, 
                    story.LeftCommentData.SuccessComment);
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultDescriptionText, 
                    story.LeftCommentData.SuccessDescription);
            }
            else
            {
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText, 
                    story.LeftCommentData.FailComment);
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultDescriptionText, 
                    story.LeftCommentData.FailDescription);
            }
            
            // 오른쪽 결과
            if (_storyModel.RightSuccess)
            {
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText, 
                    story.RightCommentData.SuccessComment);
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultDescriptionText, 
                    story.RightCommentData.SuccessDescription);
            }
            else
            {
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText, 
                    story.RightCommentData.FailComment);
                _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultDescriptionText, 
                    story.RightCommentData.FailDescription);
            }
        }
    }
}
