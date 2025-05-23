﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerModel _model;
    [SerializeField] private GameObject _stateCanvas;
    [SerializeField] private StateModel _stateModel;

    private void Awake()
    {
        _model.OnStressChanged += UpdatePlayerStress;
        _model.OnFamousChanged += UpdatePlayerFamous;
        _model.OnSubscriberChanged += UpdatePlayerSubscriber;
        _view.OnStateButtonClicked += UpdateState;
    }

    public void UpdatePlayerStress(int value)
    {
        _view.UpdatePlayerStress(value);
    }

    public void UpdatePlayerFamous(int value)
    {
        Image image = _view.GetImage((int)PlayerView.Images.FamousImage);
        Debug.Log(value);
        image.fillAmount = value / 100.0f;
    }

    public void UpdatePlayerSubscriber(int value)
    {
        _view.UpdatePlayerSubscribers(value);
    }

    private void UpdateState()
    {
        if(_stateCanvas.activeSelf)
        {
            _stateCanvas.SetActive(false);
        }
        else
        {
            _stateCanvas.SetActive(true);
            _stateModel.SetState(_model.Famous, _model.Stress);
        }
    }
}