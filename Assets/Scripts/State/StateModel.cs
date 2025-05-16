using UnityEngine;
using System;

public enum StateType
{
    One, Two, Three, Four, Five
}
public class StateModel : MonoBehaviour
{
    private StateType _curState;

    public StateType CurState => _curState;

    public event Action OnStateChanged;

    public void SetState(int famous, int stress)
    {
        if(stress> 0 && stress < 15)
            _curState = StateType.One;
        else if (stress > 80)
            _curState = StateType.Two;
        else if (famous > 0 && famous < 15)
            _curState = StateType.Three;
        else if (stress > 80)
            _curState = StateType.Four;
        else
            _curState = StateType.Five;

        OnStateChanged?.Invoke();
    }
}
