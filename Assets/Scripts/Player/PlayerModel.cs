using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _presenter;

    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;

    public int PreviousStress = 100;
    public event Action<int> OnStressChanged;
    public event Action<int> OnFamousChanged;
    public event Action<int> OnSubscriberChanged;

    public void IncreaseStress(int value)
    {
        UpdatePlayerStress(value);
    }

    public void DecreaseStress(int value)
    {
        UpdatePlayerStress(-value);
    }

    public void UpdatePlayerStress(int value)
    {
        _stress = Mathf.Clamp(_stress + value, 0, 100);
        OnStressChanged?.Invoke(_stress);
    }

    public void UpdatePlayerFamous(int value)
    {
        _famous = Mathf.Clamp(_famous + value, 0, 100);
        OnFamousChanged?.Invoke(_famous);
    }

    public void UpdatePlayerSubscriber(int value)
    {
        _subscriber = Mathf.Clamp(_subscriber + value, 0, 100);
        OnSubscriberChanged?.Invoke(_subscriber);
    }
}