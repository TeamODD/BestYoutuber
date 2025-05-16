using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _presenter;

    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;
    [SerializeField] private EndingConditionManager _endingConditionManager;

    public event Action<int> OnStressChanged;
    public event Action<int> OnFamousChanged;
    public event Action<int> OnSubscriberChanged;
    public int Stress => _stress;
    public int Famous => _famous;
    public int Subscriber => _subscriber;

    public void UpdatePlayerStress(int value)
    {
        _stress = Mathf.Clamp(_stress + value, 0, 100);
        OnStressChanged?.Invoke(_stress);
        Debug.Log($"Stress: {_stress}");
        _endingConditionManager.CheckForEnding();
    }

    public void UpdatePlayerFamous(int value)
    {
        _famous = Mathf.Clamp(_famous + value, 0, 100);
        OnFamousChanged?.Invoke(_famous);
        Debug.Log($"Famous: {_famous}");
        _endingConditionManager.CheckForEnding();
    }

    public void UpdatePlayerSubscriber(int value)
    {
        _subscriber = Mathf.Clamp(_subscriber + value, 0, 10000000);
        OnSubscriberChanged?.Invoke(_subscriber);
        _endingConditionManager.CheckForEnding();
    }
}