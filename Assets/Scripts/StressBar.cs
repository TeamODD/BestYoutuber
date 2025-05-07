using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StressBar : MonoBehaviour
{
    private float currentStress = 100f;
    private const float maxStress = 100f;
    private const float stressChangeAmount = 20f;

    [SerializeField] private Image stressImage;
    [SerializeField] private TMP_Text stressText;

    private Color stressIncreaseColor = new Color(0.10f, 0.23f, 0.34f);
    private Color stressDecreaseColor = new Color(0.18f, 0.55f, 0.25f);
    private Color originalColor;

    private void Start()
    {
        UpdateUI();
        originalColor = stressImage.color;
    }

    public void IncreaseStress()
    {
        currentStress = Mathf.Min(currentStress + stressChangeAmount, maxStress);
        ChangeColor(stressIncreaseColor);
        UpdateUI();
    }

    public void DecreaseStress()
    {
        currentStress = Mathf.Max(currentStress - stressChangeAmount, 0f);
        ChangeColor(stressDecreaseColor);
        UpdateUI();
    }

    private void UpdateUI()
    {
        stressImage.fillAmount = currentStress / maxStress;
        stressText.text = $"{(int)currentStress}";
    }

    private void ChangeColor(Color targetColor)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColorCoroutine(targetColor));
    }

    private IEnumerator ChangeColorCoroutine(Color targetColor)
    {
        stressImage.color = targetColor;
        yield return new WaitForSeconds(0.3f);
        stressImage.color = originalColor;
    }
}