using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _presenter;

    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;

    [Header("InitialStatus")]
    [SerializeField] private int _initialStress = 50;
    [SerializeField] private int _initialFamous = 50;
    [SerializeField] private int _initialSubscriber = 100;

    public event Action<int> OnStressChanged;
    public event Action<int> OnFamousChanged;
    public event Action<int> OnSubscriberChanged;

    public int Stress => _stress;
    public int Famous => _famous;
    public int Subscriber => _subscriber;

    private void Awake()
    {
        _stress = Mathf.Clamp(_initialStress, 0, 100);
        _famous = Mathf.Clamp(_initialFamous, 0, 100);
        _subscriber = Mathf.Clamp(_initialSubscriber, 0, 10000000);
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
        _subscriber = Mathf.Clamp(_subscriber + value, 0, 10000000);
        OnSubscriberChanged?.Invoke(_subscriber);
    }
}
