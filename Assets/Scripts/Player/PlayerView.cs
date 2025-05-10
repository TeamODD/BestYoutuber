using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        StressText      
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }

    private IEnumerator ChangeColorCoroutine(Image image, Color targetColor)
    {
        image.color = targetColor;
        yield return new WaitForSeconds(0.3f);
        image.color = originalColor;
    }

    public void UpdatePlayerStress(int value)
    {
        Image image = GetImage((int)Images.StressImage);
        image.fillAmount = value / 100.0f;

        TextMeshProUGUI tmp = GetTmp((int)Tmps.StressText);
        tmp.text = value.ToString();

        originalColor = image.color;

        StopAllCoroutines();
        if (value < _previousStress)
            StartCoroutine(ChangeColorCoroutine(image, stressDecreaseColor));
        else if (value > _previousStress)
            StartCoroutine(ChangeColorCoroutine(image, stressIncreaseColor));

        _previousStress = value;
    }
}
