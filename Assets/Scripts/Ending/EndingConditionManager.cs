using UnityEngine;

public class EndingConditionManager : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private SadEndingManager _sadEndingManager;

    [Header("Ending Data")]
    [SerializeField] private SadEndingData Stress100Data;
    [SerializeField] private SadEndingData Stress0Data;
    [SerializeField] private SadEndingData Famous100Data;
    [SerializeField] private SadEndingData Famous0Data;
    [SerializeField] private SadEndingData Subscriber10000000Data;

    private bool _hasEnded = false;

    public void CheckForEnding()
    {
        if (_hasEnded)
        {
            Debug.Log("CheckForEnding skipped: already ended.");
            return;
        }

        Debug.Log($"CheckForEnding called. Current status - Stress: {_playerModel.Stress}, Famous: {_playerModel.Famous}, Subscriber: {_playerModel.Subscriber}");

        if (_playerModel.Stress >= 100)
        {
            Debug.Log("Ending triggered: Stress >= 100");
            _sadEndingManager.ShowSadEnding(Stress100Data);
            _hasEnded = true;
        }
        else if (_playerModel.Stress <= 0)
        {
            Debug.Log("Ending triggered: Stress <= 0");
            _sadEndingManager.ShowSadEnding(Stress0Data);
            _hasEnded = true;
        }
        else if (_playerModel.Famous >= 100)
        {
            Debug.Log("Ending triggered: Famous >= 100");
            _sadEndingManager.ShowSadEnding(Famous100Data);
            _hasEnded = true;
        }
        else if (_playerModel.Famous <= 0)
        {
            Debug.Log("Ending triggered: Famous <= 0 (Famous value = " + _playerModel.Famous + ")");
            _sadEndingManager.ShowSadEnding(Famous0Data);
            _hasEnded = true;
        }
        else if (_playerModel.Subscriber >= 10000000)
        {
            Debug.Log("Ending triggered: Subscriber >= 10,000,000");
            _sadEndingManager.ShowSadEnding(Subscriber10000000Data);
            _hasEnded = true;
        }
        else
        {
            Debug.Log("No ending conditions met.");
        }
    }
}
