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
    [SerializeField] private FingerDetector _fingerDetector;

    public void SetNewStory(StoryData story)
    {
        _fingerDetector.canMove = true;

        _choiceView.StartTypeLineCor(_choiceView.GetTmp((int)ChoiceView.Tmps.ChoiceText), story.ChoiceText);
        //_choiceView.SetTmpText((int)ChoiceView.Tmps.ChoiceText, story.ChoiceText);
        _choiceView.SetImageSprite((int)ChoiceView.Images.ChoiceImage, story.ChoiceSprite);
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectText, story.LeftCommentData.ChoiceComment);
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectText, story.RightCommentData.ChoiceComment);

        Image leftImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.LeftSelectChanceImage);
        leftImage.fillAmount = story.LeftCommentData.SuccessChance / 100.0f;
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectChanceText,
            story.LeftCommentData.SuccessChance.ToString("f0"));

        Image rightImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.RightSelectChanceImage);
        rightImage.fillAmount = story.RightCommentData.SuccessChance / 100.0f;
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectChanceText,
            story.RightCommentData.SuccessChance.ToString("f0"));

        _storyModel.SetLeftSuccess(story.LeftCommentData.SuccessChance);
        _storyModel.SetRightSuccess(story.RightCommentData.SuccessChance);

        if (_storyModel.LeftSuccess)
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText,
                story.LeftCommentData.SuccessComment);
            _choiceResultView.SetImageSprite((int)ChoiceResultView.Images.LeftSelectChangeImage,
                story.LeftCommentData.SuccessSprite);
        }
        else
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText,
                story.LeftCommentData.FailComment);
            _choiceResultView.SetImageSprite((int)ChoiceResultView.Images.LeftSelectChangeImage,
                story.LeftCommentData.FailSprite);
        }

        if (_storyModel.RightSuccess)
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText,
                story.RightCommentData.SuccessComment);
            _choiceResultView.SetImageSprite((int)ChoiceResultView.Images.RightSelectChangeImage,
                story.RightCommentData.SuccessSprite);
        }
        else
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText,
                story.RightCommentData.FailComment);
            _choiceResultView.SetImageSprite((int)ChoiceResultView.Images.RightSelectChangeImage,
                story.RightCommentData.FailSprite);
        }
    }
}