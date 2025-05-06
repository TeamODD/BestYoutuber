using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;


public class StoryModel : MonoBehaviour
{
    [SerializeField] private StoryPresenter _presenter;

    public event Action OnStoryChanged;

    private void Start()
    {
        //OnReadyChanged += _presenter.OnReadyChanged;
    }
}
