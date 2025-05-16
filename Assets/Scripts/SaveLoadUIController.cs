// SaveLoadUIController.cs

using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUIController : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _newGameButton;
    
    //[SerializeField] private GameObject _newGameConfirmationPanel;
    
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
        // 버튼에 리스너 추가
        if (_saveButton != null)
        {
            _saveButton.onClick.AddListener(OnSaveButtonClicked);
        }
        
        if (_loadButton != null)
        {
            _loadButton.onClick.AddListener(OnLoadButtonClicked);
            
            // 저장 파일이 없으면 불러오기 버튼 비활성화
            if (SaveSystem.Instance != null)
            {
                _loadButton.interactable = SaveSystem.Instance.HasSaveFile();
            }
        }
        
        if (_newGameButton != null)
        {
            _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        }
    }
    
    // 저장 버튼 클릭
    private void OnSaveButtonClicked()
    {
        if (_gameManager != null)
        {
            _gameManager.SaveGame();
            
            // 저장 후 불러오기 버튼 활성화
            if (_loadButton != null)
            {
                _loadButton.interactable = true;
            }
        }
    }

    public void ResetGame()
    {
        SaveSystem.Instance.DeleteSave();
    
        // _playerModel.UpdatePlayerSubscriber(-_playerModel.Subscriber);
        // _playerModel.UpdatePlayerStress(-_playerModel.Stress);
        // _playerModel.UpdatePlayerFamous(-_playerModel.Famous);
    
        //SceneManager.LoadScene("StartScene"); // 또는 초기 씬 이름
    }
    
    // 불러오기 버튼 클릭
    private void OnLoadButtonClicked()
    {
        if (_gameManager != null && SaveSystem.Instance.HasSaveFile())
        {
            _gameManager.LoadGame();
        }
    }
    
    private void OnNewGameButtonClicked()
    {
        // 저장 파일이 있는 경우 확인 패널 표시
        if (SaveSystem.Instance.HasSaveFile())
        {
            //_newGameConfirmationPanel.SetActive(true);
        }
        // 저장 파일이 없는 경우 바로 새 게임 시작
        else if (_gameManager != null)
        {
            _gameManager.StartNewGame();
        }
    }
    
   
}