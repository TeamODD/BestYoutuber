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
  

    [SerializeField] private List<DialogueLine> _dialogueLines = new();
    
    private int _currentLine = 0;
    
    private void Start()
    {
        SetupDialogue();
    }
    
    private void SetupDialogue()
    {
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "옆반 걔가 유튜버로 1억벌었다는데?", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "진짜? 영상 한번 봐볼래", isPlayer = true });
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "한번 봐봐", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "이정도는 나도하겠는데?", isPlayer = true });
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "너도 유튜버 해보게?", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "유튜버 해보지 뭐. 오늘부터 꿈은 천만유튜버야!", isPlayer = true });

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
        DialogueLine currentDialogue = _dialogueLines[_currentLine];
        
        // 화자 이름 설정
        _nameText.text = currentDialogue.speakerName;
        
        // 대화 텍스트 설정
        _dialogueText.text = currentDialogue.dialogueText;
    }
}