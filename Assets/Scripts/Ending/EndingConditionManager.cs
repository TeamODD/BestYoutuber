using UnityEngine;

public class EndingConditionManager : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private SadEndingManager _sadEndingManager;

    [Header("엔딩 조건에 따른 데이터")]
    [SerializeField] private SadEndingData Stress100Data;
    [SerializeField] private SadEndingData Stress0Data;
    [SerializeField] private SadEndingData Famous100Data;
    [SerializeField] private SadEndingData Famous0Data;
    [SerializeField] private SadEndingData Subscriber10000000Data;

    private bool _hasEnded = false;

    public void CheckForEnding()
    {
        if (_hasEnded) return;

        if (_playerModel.Stress >= 100)
        {
            _sadEndingManager.ShowSadEnding(Stress100Data);
            _hasEnded = true;
        }
        else if (_playerModel.Stress <= 0)
        {
            _sadEndingManager.ShowSadEnding(Stress0Data);
            _hasEnded = true;
        }
        else if (_playerModel.Famous >= 100)
        {
            _sadEndingManager.ShowSadEnding(Famous100Data);
            _hasEnded = true;
        }
        else if (_playerModel.Famous <= 0)
        {
            _sadEndingManager.ShowSadEnding(Famous0Data);
            _hasEnded = true;
        }
        else if (_playerModel.Subscriber >= 10000000)
        {
            _sadEndingManager.ShowSadEnding(Subscriber10000000Data);
            _hasEnded = true;
        }
    }
}