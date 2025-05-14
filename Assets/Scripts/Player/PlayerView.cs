using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using NUnit.Framework.Interfaces;

public class PlayerView : ViewBase
{
    private Color originalColor;
    private Color stressIncreaseColor = new Color(0.18f, 0.55f, 0.25f);
    private Color stressDecreaseColor = new Color(0.10f, 0.23f, 0.34f);
    private int _previousStress = 100;
    private int _previousSubscriber = 0;

    [SerializeField] private float _tmpAnimDuration = 0.5f;
    [SerializeField] private float _animSpan = 0.5f;

    private Coroutine _stressCor;
    private Coroutine _subscriberCor;

    public enum Images
    {
        StressImage,
        FamousImage
    }
    
    public enum Tmps
    {
        SubscriberText,  
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }

    IEnumerator ShowResultCo(TextMeshProUGUI tmp, int preSubscribers, int curSubscribers)
    {
        SetTmpText((int)Tmps.SubscriberText, preSubscribers.ToString());

        //WaitForSeconds animWait = new WaitForSeconds(_animSpan);

        yield return TmpAnim(tmp, preSubscribers, curSubscribers);
    }

    IEnumerator TmpAnim(TextMeshProUGUI tmp, int preSubscribers, int number)
    {
        float elapsed = 0.0f;

        while (elapsed < _tmpAnimDuration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / _tmpAnimDuration;
            int currentNumber = (int)Mathf.Lerp(preSubscribers, number, normalizedTime);
            tmp.text = currentNumber.ToString();
            yield return null;
        }
        tmp.text = number.ToString();
    }

    private IEnumerator ChangeColorCoroutine(Image image, Color targetColor, int amount)
    {
        float start = image.fillAmount;
        float end = Mathf.Max(0f, amount) / 100f;
        float elapsed = 0f;

        image.color = targetColor;

        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.3f;
            image.fillAmount = Mathf.Lerp(start, end, t);
            yield return null;
        }

        image.color = originalColor;

        image.fillAmount = end;
    }

    public void UpdatePlayerStress(int value)
    {
        Image image = GetImage((int)Images.StressImage);

        originalColor = image.color;

        if(_stressCor != null) 
            StopCoroutine(_stressCor);

        if (value < _previousStress)
            _stressCor = StartCoroutine(ChangeColorCoroutine(image, stressDecreaseColor, value));
        else if (value > _previousStress)
            _stressCor = StartCoroutine(ChangeColorCoroutine(image, stressIncreaseColor, value));

        _previousStress = value;
    }

    public void UpdatePlayerSubscribers(int value)
    {
        TextMeshProUGUI tmp = GetTmp((int)Tmps.SubscriberText);

        if (_subscriberCor != null)
            StopCoroutine(_subscriberCor);

        _subscriberCor = StartCoroutine(ShowResultCo(tmp, _previousSubscriber, value));
        
        _previousSubscriber = value;
    }
}
