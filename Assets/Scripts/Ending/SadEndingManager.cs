using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SadEndingManager : MonoBehaviour
{
    [SerializeField] private Image _endingImage;
    [SerializeField] private CanvasGroup _endingCanvas;
    [SerializeField] private FingerDetector _fingerDetector;
    [SerializeField] private TextMeshProUGUI _endingDescription;

    private bool _isEnding = false;
    private SadEndingType _currentEndingType;

    public void ShowSadEnding(SadEndingData data)
    {
        Debug.Log("[SadEndingManager] ShowSadEnding »£√‚µ  ≈∏¿‘: {data.EndingType}");
        _isEnding = true;
        _currentEndingType = data.EndingType;

        _endingImage.sprite = data.EndingImage;
        _endingDescription.text = data.Description;

        _endingCanvas.alpha = 1f;
        _endingCanvas.blocksRaycasts = true;
        _endingCanvas.interactable = true;

        _fingerDetector.EnableEndingSwipe(true);
    }

    public void HandleSwipeResult(bool left)
    {
        if (!_isEnding) return;

        _isEnding = false;

        if (left)
        {
            SceneLoader.LoadScene("MainMenu");
        }
        else
        {
            SceneLoader.LoadScene(_currentEndingType.ToString());
        }
    }
}
