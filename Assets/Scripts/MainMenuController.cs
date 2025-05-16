using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _exitButton;
    
    [Header("UI Panels")]
    [SerializeField] private GameObject _confirmationPanel;
    
    [Header("Confirmation Dialog")]
    [SerializeField] private Button _confirmYesButton;
    [SerializeField] private Button _confirmNoButton;
    [SerializeField] private Text _confirmationText;
    
    private void Start()
    {
        // GameManager 초기화 확인
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager가 씬에 없습니다. 생성합니다.");
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }
        
        // SaveSystem 초기화 확인
        if (SaveSystem.Instance == null)
        {
            Debug.LogWarning("SaveSystem이 씬에 없습니다. 생성합니다.");
            GameObject saveSystemObj = new GameObject("SaveSystem");
            saveSystemObj.AddComponent<SaveSystem>();
        }
        
        // 버튼 리스너 설정
        SetupButtonListeners();
        
        // 저장 데이터 유무에 따라 UI 상태 업데이트
        UpdateUIState();
    }
    
    private void SetupButtonListeners()
    {
        // 시작 버튼 (저장 데이터 있으면 이어하기, 없으면 새 게임)
        if (_startButton != null)
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }
        
        // 이어하기 버튼 (저장 데이터 불러오기)
        if (_continueButton != null)
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }
        
        // 새 게임 버튼 (저장 데이터 있으면 확인 후 초기화)
        if (_newGameButton != null)
        {
            _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        }
        
        // 종료 버튼
        if (_exitButton != null)
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        
        // 확인 패널 버튼
        if (_confirmYesButton != null)
        {
            _confirmYesButton.onClick.AddListener(OnConfirmYesClicked);
        }
        
        if (_confirmNoButton != null)
        {
            _confirmNoButton.onClick.AddListener(OnConfirmNoClicked);
        }
    }
    
    private void UpdateUIState()
    {
        bool hasSaveFile = SaveSystem.Instance.HasSaveFile();
        
        // 저장 파일이 있으면 이어하기 버튼 활성화
        if (_continueButton != null)
        {
            _continueButton.interactable = hasSaveFile;
        }
    }
    
    // 시작 버튼 클릭 (저장 데이터 있으면 이어하기, 없으면 새 게임)
    private void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }
    
    // 이어하기 버튼 클릭
    private void OnContinueButtonClicked()
    {
        if (SaveSystem.Instance.HasSaveFile())
        {
            // 게임 씬으로 바로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
        }
        else
        {
            Debug.LogWarning("저장 파일이 없습니다!");
        }
    }
    
    // 새 게임 버튼 클릭
    private void OnNewGameButtonClicked()
    {
        // 저장 파일이 있는 경우 확인 대화상자 표시
        if (SaveSystem.Instance.HasSaveFile() && _confirmationPanel != null)
        {
            _confirmationPanel.SetActive(true);
            
            if (_confirmationText != null)
            {
                _confirmationText.text = "저장된 게임이 있습니다. 새 게임을 시작하면 기존 데이터가 삭제됩니다. 계속하시겠습니까?";
            }
        }
        else
        {
            // 저장 파일이 없는 경우 바로 새 게임 시작
            GameManager.Instance.StartNewGame();
        }
    }
    
    // 종료 버튼 클릭
    private void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // 확인 대화상자 - 예 버튼
    private void OnConfirmYesClicked()
    {
        // 저장 파일 삭제 후 새 게임 시작
        GameManager.Instance.StartNewGame();
        
        // 확인 패널 닫기
        if (_confirmationPanel != null)
        {
            _confirmationPanel.SetActive(false);
        }
    }
    
    private void OnConfirmNoClicked()
    {
        // 확인 패널 닫기
        if (_confirmationPanel != null)
        {
            _confirmationPanel.SetActive(false);
        }
    }
}