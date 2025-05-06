using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryPresenter : MonoBehaviour
{
    [SerializeField] private ChoiceView _choiceView;
    [SerializeField] private ChoiceSelectView _choiceSelectView;
    [SerializeField] private StoryModel _model;

    [SerializeField] private GameObject leftSelectChanceGroup;
    [SerializeField] private GameObject rightSelectChanceGroup;

    private void Start()
    {
        //_view.OnChangeModelButtonClicked += UpdateModelButton;
        //_view.OnChangeBodyButtonClicked += UpdateBodyButton;
        //_view.OnChangeHeadButtonClicked += UpdateHeadButton;
        //_view.OnReadyButtonClicked += UpdateReadyButton;
    }

    public void SetNewStory(StoryData story)
    {
        leftSelectChanceGroup.SetActive(true);
        rightSelectChanceGroup.SetActive(true);

        _choiceView.SetTmpText((int)ChoiceView.Tmps.ChoiceText, story.ChoiceText);
        _choiceView.SetImageSprite((int)ChoiceView.Images.ChoiceImage, story.ChoiceSprite);
        _choiceView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectText, story.SuccessCommentModel.ChoiceComment);
        _choiceView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectText, story.FailCommentModel.ChoiceComment);

        Image leftImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.LeftSelectChanceImage);
        leftImage.fillAmount = story.SuccessCommentModel.SuccessChance / 100.0f;
        _choiceView.SetTmpText((int)ChoiceSelectView.Tmps.LeftSelectChanceText, story.SuccessCommentModel.SuccessChance.ToString("f0"));

        Image rightImage = _choiceSelectView.GetImage((int)ChoiceSelectView.Images.RightSelectChanceImage);
        rightImage.fillAmount = story.FailCommentModel.SuccessChance / 100.0f;
        _choiceView.SetTmpText((int)ChoiceSelectView.Tmps.RightSelectChanceText, story.FailCommentModel.SuccessChance.ToString("f0"));

        leftSelectChanceGroup.SetActive(false);
        rightSelectChanceGroup.SetActive(false);

        Debug.Log(ChoiceSelectView.Tmps.LeftSelectText);
        Debug.Log(ChoiceSelectView.Tmps.RightSelectText);
        Debug.Log(ChoiceSelectView.Tmps.LeftSelectChanceText);
        Debug.Log(ChoiceSelectView.Tmps.RightSelectChanceText);
        Debug.Log(ChoiceSelectView.Images.LeftSelectChanceImage);
        Debug.Log(ChoiceSelectView.Images.RightSelectChanceImage);
        
        Debug.Log(story.ChoiceText);
        Debug.Log(story.SuccessCommentModel.ChoiceComment);
        Debug.Log(story.FailCommentModel.ChoiceComment);
        Debug.Log(story.SuccessCommentModel.SuccessChance.ToString());
        Debug.Log(story.FailCommentModel.SuccessChance.ToString());
    }
}
