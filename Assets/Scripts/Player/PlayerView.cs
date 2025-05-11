using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerView : ViewBase
{
    private Color originalColor;
    private Color stressIncreaseColor = new Color(0.18f, 0.55f, 0.25f);
    private Color stressDecreaseColor = new Color(0.10f, 0.23f, 0.34f);
    private int _previousStress = 100;

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

        StopAllCoroutines();
        if (value < _previousStress)
            StartCoroutine(ChangeColorCoroutine(image, stressDecreaseColor, value));
        else if (value > _previousStress)
            StartCoroutine(ChangeColorCoroutine(image, stressIncreaseColor, value));

        _previousStress = value;
    }
}
