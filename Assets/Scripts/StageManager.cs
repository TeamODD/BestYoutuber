using System.ComponentModel;
using UnityEngine;

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
        _curStoryData = _storySelector.GetStory(_playerModel.Subscriber);
        _storyPresenter.SetNewStory(_curStoryData);
    }
}