using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;


public class StoryModel : MonoBehaviour
{
    [SerializeField] private StoryPresenter _presenter;
    [SerializeField] private PlayerModel _playerModel;
    
    private bool _leftSuccess;
    private bool _rightSuccess;

    public bool LeftSuccess => _leftSuccess;
    public bool RightSuccess => _rightSuccess;

    public void SetLeftSuccess(int value)
    {
        _leftSuccess = GetResult(value);
    }
    public void SetRightSuccess(int value)
    {
        _rightSuccess = GetResult(value);
    }

    public void UpdatePlayerUI(bool left)
    {
        StoryData storyData = StageManager.instance.CurStoryData;

        if(left)
        {
            if (_leftSuccess)
            {
                _playerModel.UpdatePlayerFamous(storyData.LeftCommentData.SuccessFamousIncrease);
                _playerModel.UpdatePlayerStress(storyData.LeftCommentData.SuccessStressDecrease);
                _playerModel.UpdatePlayerSubscriber(storyData.LeftCommentData.SuccessSubscriberIncrease);
                
                AudioManager.Instance.PlaySfx(storyData.LeftCommentData.SuccessResultSound);
            }
            else
            {
                _playerModel.UpdatePlayerFamous(storyData.LeftCommentData.FailFamousDecrease);
                _playerModel.UpdatePlayerStress(storyData.LeftCommentData.FailStressIncrease);
                _playerModel.UpdatePlayerSubscriber(storyData.LeftCommentData.FailSubscriberDecrease);
                
                AudioManager.Instance.PlaySfx(storyData.LeftCommentData.FailResultSound);
            }
        }
        else
        {
            if (_rightSuccess)
            {
                _playerModel.UpdatePlayerFamous(storyData.RightCommentData.SuccessFamousIncrease);
                _playerModel.UpdatePlayerStress(storyData.RightCommentData.SuccessStressDecrease);
                _playerModel.UpdatePlayerSubscriber(storyData.RightCommentData.SuccessSubscriberIncrease);
                
                AudioManager.Instance.PlaySfx(storyData.RightCommentData.SuccessResultSound);
            }
            else
            {
                _playerModel.UpdatePlayerFamous(storyData.RightCommentData.FailFamousDecrease);
                _playerModel.UpdatePlayerStress(storyData.RightCommentData.FailStressIncrease);
                _playerModel.UpdatePlayerSubscriber(storyData.RightCommentData.FailSubscriberDecrease);
                
                AudioManager.Instance.PlaySfx(storyData.RightCommentData.FailResultSound);
            }
        }
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
