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
        
        DisplayCurrentLine();
    }
    
    private void SetupDialogue()
    {
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "옆반 얘가 유튜브로 1억벌었다는데?", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "진짜? 한번봐볼래", isPlayer = true });
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "이거봐봐", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "아니 이정도면 나도 할 수 있을것같은데?", isPlayer = true });
        _dialogueLines.Add(new DialogueLine { speakerName = "친구", dialogueText = "정말? 쉽지않을텐데?", isPlayer = false });
        _dialogueLines.Add(new DialogueLine { speakerName = "미소녀", dialogueText = "이제부터 내꿈은 유튜버야!", isPlayer = true });
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
        

    }
}