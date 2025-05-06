using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerModel _model;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdatePlayerStress(5);
            UpdatePlayerFamous(5);
            UpdatePlayerSubscriber(5);
        }
    }

    public void UpdatePlayerStress(int value)
    {
        Image image = _view.GetImage((int)PlayerView.Images.StressImage);
        image.fillAmount = (_model.Stress-value)/100.0f;
        _model.SetStress(value);
    }
    public void UpdatePlayerFamous(int value)
    {
        Image image = _view.GetImage((int)PlayerView.Images.FamousImage);
        image.fillAmount = (_model.Famous - value) / 100.0f;
        _model.SetFamous(value);
    }

    public void UpdatePlayerSubscriber(int value)
    {
        TextMeshProUGUI tmp = _view.GetTmp((int)PlayerView.Tmps.SubscriberText);
        tmp.text = (_model.Stress - value).ToString();
        _model.SetSubscriber(value);
    }
}
