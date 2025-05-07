using System.ComponentModel;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] private StoryData[] _storyModels;
    [SerializeField] private StoryPresenter _storyPresenter;
    [SerializeField] private PlayerPresenter _playerPresenter;

    [SerializeField] private PlayerModel _playerModel;   //PlayerModel 연결 추가
         
    [SerializeField] private StoryData _curStoryData;
    public StoryData CurStoryData=> _curStoryData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _storyPresenter.SetPlayerModel(_playerModel);
        GameStart();
    }

    public void GameStart()
    {
        _storyPresenter.SetNewStory(_storyModels[0]);
    }
    public void SetNewStory()
    {
        _curStoryData = _storyModels[_curStoryData.Index + 1];
        _storyPresenter.SetNewStory(_curStoryData);
    }
}
