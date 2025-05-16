using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SimpleDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        public string dialogueText;
        public bool isPlayer; 
    }
    
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private Image _playerImage;
    [SerializeField] private Image _otherImage;

    [SerializeField] private List<DialogueLine> _dialogueLines = new();
    
    private int _currentLine = 0;
    
    private void Start()
    {
        SetupDialogue();
        
        DisplayCurrentLine();
    }
    
    private void SetupDialogue()
    {
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "안녕! 오늘 방송 준비는 다 됐어?", isPlayer = true });
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "응, 다 준비됐어. 근데 오늘 주제는 뭐야?", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "요즘 유행하는 메이크업 챌린지 할거야!", isPlayer = true });
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "그거 재밌겠다! 도와줄게.", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "고마워! 시작하자!", isPlayer = true });
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine++;
            
            if (_currentLine >= _dialogueLines.Count)
            {
                SceneManager.LoadSceneAsync("Scene0"); 
            }
            else
            {
                DisplayCurrentLine();
            }
        }
    }
    
    private void DisplayCurrentLine()
    {
        DialogueLine line = _dialogueLines[_currentLine];
        
        _nameText.text = line.speakerName;
        
        _dialogueText.text = line.dialogueText;
        
        if (line.isPlayer)
        {
            _playerImage.color = Color.white; // 활성 상태
            _otherImage.color = new Color(0.7f, 0.7f, 0.7f); // 비활성 상태
        }
        else
        {
            _playerImage.color = new Color(0.7f, 0.7f, 0.7f); // 비활성 상태
            _otherImage.color = Color.white; // 활성 상태
        }
    }
}