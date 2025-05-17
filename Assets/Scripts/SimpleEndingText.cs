using System.Collections;
using UnityEngine;
using TMPro;

public class SimpleEndingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private string _endingText;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private Canvas _textCanvas;
    [SerializeField] private Canvas _endingCanvas;
    
    private void Start()
    {
        StartCoroutine(TypeText());
        _endingCanvas.gameObject.SetActive(false);
    }
    
    private IEnumerator TypeText()
    {
        _textDisplay.text = "";
        
        for (int i = 0; i < _endingText.Length; i++)
        {
            _textDisplay.text += _endingText[i];
            yield return new WaitForSeconds(_typingSpeed);
        }
        
        yield return new WaitForSeconds(1.0f);
        
        if (_textCanvas != null)
        {
            _textCanvas.gameObject.SetActive(false);
            _endingCanvas.gameObject.SetActive(true);
        }
    }
}