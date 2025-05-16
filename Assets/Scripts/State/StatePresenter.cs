using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePresenter : MonoBehaviour
{
    [SerializeField] private StateData[] _stateDatas;

    [SerializeField] private StateModel _model;
    [SerializeField] private StateView _view;

    [SerializeField] private GameObject _commentScrollView;

    private void Awake()
    {
        _model.OnStateChanged += UpdateCharacterStateImage;
        _model.OnStateChanged += UpdateCharacterStateTmp;
        _view.OnCommentActiveButtonClicked += UpdateCommentWatchButton;
        _view.OnDisabled += ResetScrollView;
    }

    private void UpdateCharacterStateImage()
    {
        _view.GetImage((int)StateView.Images.CharacterStateImage).sprite = _stateDatas[(int)_model.CurState].CharacterStateSprite;

    }
    private void UpdateCharacterStateTmp()
    {
        _view.GetTmp((int)StateView.Tmps.CharacterStateText).text = "상태 : " + _stateDatas[(int)_model.CurState].CharacterStateText;
    }
    private void UpdateCommentWatchButton()
    {     
        if(_commentScrollView.activeSelf)
        {
            _view.GetTmp((int)StateView.Tmps.CommentWatchText).text = "댓글 보기";
            _commentScrollView.SetActive(false);
        }
        else
        {
            _view.GetTmp((int)StateView.Tmps.CommentWatchText).text = "댓글 숨기기";
            _commentScrollView.SetActive(true);
        }
    }
    private void ResetScrollView()
    {
        _view.GetTmp((int)StateView.Tmps.CommentWatchText).text = "댓글 보기";
        _commentScrollView.SetActive(false);
    }
}
