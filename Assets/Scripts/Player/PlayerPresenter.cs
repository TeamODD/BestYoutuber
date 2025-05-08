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

    public IEnumerator ChangeColorCoroutine(Image image, Color targetColor)
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