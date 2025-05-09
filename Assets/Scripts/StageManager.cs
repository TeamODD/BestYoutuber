using System.ComponentModel;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] private StoryData[] _storyDatas;
    [SerializeField] private StoryPresenter _storyPresenter;
    [SerializeField] private PlayerPresenter _playerPresenter;

    [SerializeField] private PlayerModel _playerModel;   //PlayerModel 연결 추가
         
    private StoryData _curStoryData;
    public StoryData CurStoryData=> _curStoryData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        _curStoryData = _storyDatas[0];
        _storyPresenter.SetNewStory(_curStoryData);
    }
    public void SetNewStory()
    {
        _curStoryData = _storyDatas[_curStoryData.Index + 1];
        _storyPresenter.SetNewStory(_curStoryData);
    }
}
