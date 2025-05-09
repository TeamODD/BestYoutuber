using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryPresenter : MonoBehaviour
{
    [SerializeField] private ChoiceView _choiceView;
    [SerializeField] private ChoiceSelectView _choiceSelectView;
    [SerializeField] private ChoiceResultView _choiceResultView;
    [SerializeField] private StoryModel _storyModel;

    [SerializeField] private PlayerModel _playerModel; //PlayerModel 연결

    [SerializeField] private FingerDetector _fingerDetector;

    public void SetNewStory(StoryData story)
    {
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
  
        if (_storyModel.LeftSuccess)
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText, story.LeftCommentData.SuccessComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultDescriptionText, story.LeftCommentData.SuccessDescription);
            _playerModel.UpdatePlayerStress(story.LeftCommentData.SuccessStressDecrease);
        }
        else
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText, story.LeftCommentData.FailComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultDescriptionText, story.LeftCommentData.FailDescription);
            _playerModel.UpdatePlayerStress(story.LeftCommentData.FailStressIncrease);
        }

        if (_storyModel.RightSuccess)
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText, story.RightCommentData.SuccessComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultDescriptionText, story.RightCommentData.SuccessDescription);
            _playerModel.UpdatePlayerStress(story.RightCommentData.SuccessStressDecrease);
        }
        else
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText, story.RightCommentData.FailComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultDescriptionText, story.RightCommentData.FailDescription);
            _playerModel.UpdatePlayerStress(story.RightCommentData.FailStressIncrease);
        }
    }

    public void SetPlayerModel(PlayerModel model)
    {
        _playerModel = model;
    }
}
