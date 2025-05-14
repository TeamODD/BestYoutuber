using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerModel _model;

    private void Awake()
    {
        _model.OnStressChanged += UpdatePlayerStress;
        _model.OnFamousChanged += UpdatePlayerFamous;
        _model.OnSubscriberChanged += UpdatePlayerSubscriber;
    }

    public void UpdatePlayerStress(int value)
    {
        _view.UpdatePlayerStress(value);
    }

    public void UpdatePlayerFamous(int value)
    {
        Image image = _view.GetImage((int)PlayerView.Images.FamousImage);
        image.fillAmount = value / 100.0f;
    }

    public void UpdatePlayerSubscriber(int value)
    {
        _view.UpdatePlayerSubscribers(value);
    }
}