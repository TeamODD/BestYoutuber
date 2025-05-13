using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SadEndingManager : MonoBehaviour
{
    [Header("UI ���")]
    [SerializeField] private CanvasGroup _endingCanvas;
    [SerializeField] private Image _endingImage;
    [SerializeField] private TextMeshProUGUI _endingDescription;
    [SerializeField] private FingerDetector _fingerDetector;


    private bool _isEnding = false;
    private string _currentEndingType;

    public void ShowSadEnding(SadEndingData data)
    {
        Debug.Log($"[SadEndingManager] ShowSadEnding ȣ��� - EndingType: {data.EndingType}");

        _isEnding = true;
        _currentEndingType = data.EndingType.ToString();

        _endingImage.sprite = data.EndingImage;
        _endingDescription.text = data.Description;

        _endingCanvas.alpha = 1f;
        _endingCanvas.blocksRaycasts = true;
        _endingCanvas.interactable = true;
        _fingerDetector.EnableEndingSwipe(true);

    }


    public void HandleSwipeResult(bool isLeft)
    {
        if (isLeft)
        {
            Debug.Log("[SadEndingManager] ���� �������� �� MainMenu�� �̵�");
            SceneManager.LoadScene("MainMenu"); 
        }
        else
        {
            Debug.Log("[SadEndingManager] ������ �������� �� ���� �� �����");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
