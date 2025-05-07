using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerModel _model;

    private Color originalColor;
    private Color stressIncreaseColor = new Color(0.18f, 0.55f, 0.25f); // 초록색
    private Color stressDecreaseColor = new Color(0.10f, 0.23f, 0.34f); // 파란색

    private void Awake()
    {
        _model.OnStressChanged += UpdatePlayerStress;
        _model.OnFamousChanged += UpdatePlayerFamous;
        _model.OnSubscriberChanged += UpdatePlayerSubscriber;
    }

    public void UpdatePlayerStress(int value)
    {
        Image image = _view.GetImage((int)PlayerView.Images.StressImage);
        image.fillAmount = value / 100.0f;

        TextMeshProUGUI tmp = _view.GetTmp((int)PlayerView.Tmps.StressText);
        tmp.text = value.ToString();

        if (originalColor == default)
            originalColor = image.color;

        // 색상 변화 적용
        StopAllCoroutines();
        if (value < _model.PreviousStress)
            StartCoroutine(ChangeColorCoroutine(image, stressDecreaseColor));
        else if (value > _model.PreviousStress)
            StartCoroutine(ChangeColorCoroutine(image, stressIncreaseColor));

        _model.PreviousStress = value;
    }

    private IEnumerator ChangeColorCoroutine(Image image, Color targetColor)
    {
        image.color = targetColor;
        yield return new WaitForSeconds(0.3f);
        image.color = originalColor;
    }

    public void UpdatePlayerFamous(int value)
    {
        Image image = _view.GetImage((int)PlayerView.Images.FamousImage);
        image.fillAmount = value / 100.0f;
    }

    public void UpdatePlayerSubscriber(int value)
    {
        TextMeshProUGUI tmp = _view.GetTmp((int)PlayerView.Tmps.SubscriberText);
        tmp.text = value.ToString();
    }
}