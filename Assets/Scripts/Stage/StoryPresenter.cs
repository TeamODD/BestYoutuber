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
    [SerializeField] private StoryModel _model;

    [SerializeField] private FingerDetector _fingerDetector;

    public void SetNewStory(StoryData story)
    {
        _fingerDetector.canMove = true;

        _choiceView.SetTmpText((int)ChoiceView.Tmps.ChoiceText, story.ChoiceText);
        _choiceView.SetImageSprite((int)ChoiceView.Images.ChoiceImage, story.ChoiceSprite);
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectText, story.SuccessCommentModel.ChoiceComment);
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectText, story.FailCommentModel.ChoiceComment);

        Image leftImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.LeftSelectChanceImage);
        leftImage.fillAmount = story.SuccessCommentModel.SuccessChance / 100.0f;
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectChanceText, story.SuccessCommentModel.SuccessChance.ToString("f0"));

        Image rightImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.RightSelectChanceImage);
        rightImage.fillAmount = story.FailCommentModel.SuccessChance / 100.0f;
        _choiceSelectView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectChanceText, story.FailCommentModel.SuccessChance.ToString("f0"));

        if(GetResult(story.SuccessCommentModel.SuccessChance))
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText, story.SuccessCommentModel.SuccessComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultDescriptionText, story.SuccessCommentModel.SuccessDescription);
        }
        else
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultCommentText, story.SuccessCommentModel.FailComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.LeftResultDescriptionText, story.SuccessCommentModel.FailDescription);
        }

        if (GetResult(story.FailCommentModel.SuccessChance))
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText, story.FailCommentModel.SuccessComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultDescriptionText, story.FailCommentModel.SuccessDescription);
        }
        else
        {
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultCommentText, story.FailCommentModel.FailComment);
            _choiceResultView.SetTmpText((int)ChoiceResultView.Tmps.RightResultDescriptionText, story.FailCommentModel.FailDescription);
        }

        //Debug.Log(ChoiceSelectView.Tmps.LeftSelectText);
        //Debug.Log(ChoiceSelectView.Tmps.RightSelectText);
        //Debug.Log(ChoiceSelectView.Tmps.LeftSelectChanceText);
        //Debug.Log(ChoiceSelectView.Tmps.RightSelectChanceText);
        //Debug.Log(ChoiceSelectView.Images.LeftSelectChanceImage);
        //Debug.Log(ChoiceSelectView.Images.RightSelectChanceImage);
        //
        //Debug.Log(story.ChoiceText);
        //Debug.Log(story.SuccessCommentModel.ChoiceComment);
        //Debug.Log(story.FailCommentModel.ChoiceComment);
        //Debug.Log(story.SuccessCommentModel.SuccessChance.ToString());
        //Debug.Log(story.FailCommentModel.SuccessChance.ToString()); 
    }

    private bool GetResult(int chance)
    {
        int random = Random.Range(0, 100);

        if (random < chance)
            return true;
        else
            return false;
    }
}
