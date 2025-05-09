using TMPro;
using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _presenter;

    [SerializeField] private int _stress;
    [SerializeField] private int _famous;
    [SerializeField] private int _subscriber;
    private Color originalColor;
    private Color stressIncreaseColor = new Color(0.18f, 0.55f, 0.25f); // 초록색
    private Color stressDecreaseColor = new Color(0.10f, 0.23f, 0.34f); // 파란색

    public event Action<int> OnStressChanged;
    public event Action<int> OnFamousChanged;
    public event Action<int> OnSubscriberChanged;

    public void SetPlayerStress(int value)             // 이미지 stress 불러오고 fiillamount 값 100f로 지정 TMP StressText 가져와서 값지정 색상변경
    {
        Image image = _view.GetImage((int)PlayerView.Images.StressImage);
        image.fillAmount = value / 100.0f;

        TextMeshProUGUI tmp = _view.GetTmp((int)PlayerView.Tmps.StressText);
        tmp.text = value.ToString();

        if (originalColor == default)
            originalColor = image.color;

        // 색상 변화 적용
        StopAllCoroutines();
        if (value < 0)
            StartCoroutine(ChangeColorCoroutine(image, stressDecreaseColor));
        else if (value > 0)
            StartCoroutine(ChangeColorCoroutine(image, stressIncreaseColor));
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