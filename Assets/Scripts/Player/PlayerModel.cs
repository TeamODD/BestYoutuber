using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _presenter;

    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;

    public event Action<int> OnStressChanged;
    public event Action<int> OnFamousChanged;
    public event Action<int> OnSubscriberChanged;

    public void UpdatePlayerStress(int value)
    {
        if (_stress + value < 0)
            _stress = 0;
        else if (_stress + value > 100)
            _stress = 100;
        else
            _stress += value;

        OnStressChanged.Invoke(_stress);
    }
    public void UpdatePlayerFamous(int value)
    {
        if (_famous + value < 0)
            _famous = 0;
        else if (_famous + value > 100)
            _famous = 100;
        else
            _famous += value;

        OnFamousChanged.Invoke(_famous);
    }
    public void UpdatePlayerSubscriber(int value)
    {
        if (_subscriber + value < 0)
            _subscriber = 0;
        else if (_subscriber + value > 100)
            _subscriber = 100;
        else
            _subscriber += value;

        OnSubscriberChanged.Invoke(_subscriber);
    }   
}
