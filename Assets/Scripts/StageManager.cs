using System.ComponentModel;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] private StoryData[] _storyModels;
    [SerializeField] private StoryPresenter _storyPresenter;
    [SerializeField] private PlayerPresenter _playerPresenter;

    [SerializeField] private StoryData _curStoryData;
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
        _storyPresenter.SetNewStory(_storyModels[0]);
    }
    public void SetNewStory()
    {
        _storyPresenter.SetNewStory(_storyModels[_curStoryData.Index + 1]);
    }
    //public void 
}
