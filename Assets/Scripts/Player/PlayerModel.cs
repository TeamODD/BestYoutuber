using TMPro;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _presenter;
    [SerializeField] private PlayerView _view;

    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;

    private Color originalColor;
    private Color stressIncreaseColor = new Color(0.18f, 0.55f, 0.25f); // 초록색
    private Color stressDecreaseColor = new Color(0.10f, 0.23f, 0.34f); // 파란색

    public event Action<int> OnStressChanged;
    public event Action<int> OnFamousChanged;
    public event Action<int> OnSubscriberChanged;

    public IEnumerator ChangeColorCoroutine(Image image, Color targetColor)
    {
        image.color = targetColor;
        yield return new WaitForSeconds(0.3f);
        image.color = originalColor;
    }

    private void Update()
    {
        // Debug.Log("Stress: " + _stress);
        // Debug.Log("Famous: " + _famous);
        // Debug.Log("Subscriber: " + _subscriber);
    }


    public void UpdatePlayerStress(int value)
    {
        //문제점 : 텍스트 넘길때마다 색상이 무조건 변화함. 
        //하지만 value 값이 0일때는 색상이 변하지 않음
        var oldStress = _stress;
        _stress = Mathf.Clamp(_stress + value, 0, 100);
        var image = _view.GetImage((int)PlayerView.Images.StressImage);

        if (originalColor == default)
            originalColor = image.color;

        if (_stress != oldStress)
        {
            StopAllCoroutines();
            if (value < 0)
                StartCoroutine(ChangeColorCoroutine(image, stressDecreaseColor));
            else if (value > 0)
                StartCoroutine(ChangeColorCoroutine(image, stressIncreaseColor));
        }
        OnStressChanged?.Invoke(_stress);
        Debug.Log("Stress Update");
        Debug.Log("Stress: " + _stress);
        Debug.Log("Value: " + value);
        Debug.Log("Invoke StressChanged");
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