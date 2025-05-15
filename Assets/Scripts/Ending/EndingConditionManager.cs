using UnityEngine;

public class EndingConditionManager : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;

    public enum EndingType
    {
        Happy,
        Sad1,
        Sad2,
        Sad3,
        Sad4
    }

    public EndingType GetEnding()
    {
        int stress = _playerModel.Stress;
        int popularity = _playerModel.Famous;      
        int subscribers = _playerModel.Subscriber; 

        if (stress < 30 && subscribers >= 1000)
            return EndingType.Happy;
        else if (stress > 80)
            return EndingType.Sad1;
        else if (popularity < 20)
            return EndingType.Sad2;
        else if (subscribers < 200)
            return EndingType.Sad3;
        else
            return EndingType.Sad4;
    }
}
