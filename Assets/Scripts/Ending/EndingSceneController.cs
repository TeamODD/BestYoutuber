using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image _endingImage;
    [SerializeField] private TextMeshProUGUI _endingDescription;

    private void Start()
    {
        SadEndingData data = SadEndingDataProvider.CurrentData;

        if (data == null)
        {
            Debug.LogError(" EndingSceneController: SadEndingData가 null입니다.");
            return;
        }

        _endingImage.sprite = data.EndingImage;
        _endingDescription.text = data.Description;
    }
}
