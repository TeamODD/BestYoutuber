using UnityEngine;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private ResultView _resultView;

    [Header("테스트 데이터")]
    [SerializeField] private int _testData1;
    [SerializeField] private int _testData2;
    [SerializeField] private int _testData3;

    private void Awake()
    {
        _resultView = GetComponent<ResultView>();
        _resultView.OnPanelClicked += () => gameObject.SetActive(false);
    }

    private void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        _resultView.ShowResult(_testData1, _testData2, _testData3);
    }
}
