using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance;
    
    // 참조할 컴포넌트들
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private StorySelector _storySelector;
    [SerializeField] private NotificationSystem _notificationSystem;
    
    // 게임 초기 설정값
    [Header("Initial Values")]
    [SerializeField] private int _initialSubscriber = 100;
    [SerializeField] private int _initialStress = 50;
    [SerializeField] private int _initialFamous = 50;
    
    // 자동 저장 관련 설정
    [Header("Auto Save Settings")]
    [SerializeField] private bool _enableAutoSave = true;
    [SerializeField] private float _autoSaveInterval = 60f; // 60초마다 자동저장
    
    private float _timeSinceLastSave;
    
    // 씬 이름 상수
    private const string MAIN_MENU_SCENE = "MainMenu";
    private const string INTRO_SCENE = "Scene0";
    private const string GAME_SCENE = "Scene1";
    
    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // 필요한 컴포넌트 자동 찾기 (인스펙터에서 설정되지 않은 경우)
        if (_playerModel == null)
            _playerModel = FindObjectOfType<PlayerModel>();
            
        if (_storySelector == null)
            _storySelector = FindObjectOfType<StorySelector>();
            
        if (_notificationSystem == null)
            _notificationSystem = FindObjectOfType<NotificationSystem>();
    }
    
    private void Start()
    {
        _timeSinceLastSave = 0f;
        
        // 씬 로드 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDestroy()
    {
        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void Update()
    {
        // 자동 저장 로직
        if (_enableAutoSave)
        {
            _timeSinceLastSave += Time.deltaTime;
            
            if (_timeSinceLastSave >= _autoSaveInterval)
            {
                SaveGame();
                _timeSinceLastSave = 0f;
            }
        }
    }
    
    // 씬 로드 시 호출되는 이벤트 핸들러
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GAME_SCENE)
        {
            // 게임 씬이 로드되었을 때 필요한 초기화 작업
            InitializeGameComponents();
            
            // 저장 파일 여부에 따라 데이터 로드 또는 초기화
            if (SaveSystem.Instance.HasSaveFile())
            {
                LoadGame();
            }
        }
    }
    
    // 게임 컴포넌트 초기화 (씬 전환 후 참조가 끊어질 수 있음)
    private void InitializeGameComponents()
    {
        // 필요한 컴포넌트 다시 찾기
        if (_playerModel == null)
            _playerModel = FindObjectOfType<PlayerModel>();
            
        if (_storySelector == null)
            _storySelector = FindObjectOfType<StorySelector>();
            
        if (_notificationSystem == null)
            _notificationSystem = FindObjectOfType<NotificationSystem>();
    }
    
    // 게임 시작 메서드 (메인 메뉴에서 호출)
    public void StartGame()
    {
        if (SaveSystem.Instance.HasSaveFile())
        {
            // 저장 파일이 있으면 바로 게임 씬으로
            SceneManager.LoadScene(GAME_SCENE);
        }
        else
        {
            // 저장 파일이 없으면 인트로 씬부터 시작
            StartCoroutine(StartNewGameRoutine());
        }
    }
    
    // 새 게임 시작 루틴
    private IEnumerator StartNewGameRoutine()
    {
        // 인트로 씬 로드
        SceneManager.LoadScene(INTRO_SCENE);
        
        // 인트로 지속 시간 (필요에 따라 조정)
        yield return new WaitForSeconds(3.0f);
        
        // 게임 씬으로 전환
        SceneManager.LoadScene(GAME_SCENE);
    }
    
    // 새 게임 시작 (기존 데이터 초기화)
    public void StartNewGame()
    {
        // 저장 파일 삭제
        SaveSystem.Instance.DeleteSave();
        
        // 플레이어 데이터 초기화
        ResetPlayerData();
        
        // 인트로부터 시작
        StartCoroutine(StartNewGameRoutine());
    }
    
    // 게임 저장
    public void SaveGame()
    {
        // 먼저 컴포넌트 참조 확인
        InitializeGameComponents();
        
        if (_playerModel == null)
        {
            Debug.LogError("저장 실패: PlayerModel을 찾을 수 없습니다.");
            return;
        }
        
        // GameSaveData 객체 생성
        GameSaveData saveData = new GameSaveData
        {
            // 플레이어 상태 저장
            subscriber = _playerModel.Subscriber,
            stress = _playerModel.Stress,
            famous = _playerModel.Famous,
            
            // 현재 스토리 티어 저장
            currentStoryTier = (int)GetCurrentStoryTier(),
            
            // 발견한 히든 스토리 목록 저장
            discoveredHiddenStories = GetDiscoveredHiddenStories(),
            
            // 알림 상태 저장
            shownNotifications = GetShownNotifications(),
            
            // 버튼 상태 저장 (예시, 실제 값은 어디서 관리하는지에 따라 다름)
            silverButtonAchieved = _playerModel.Subscriber >= 100000,
            goldButtonAchieved = _playerModel.Subscriber >= 1000000
        };
        
        // SaveSystem을 통해 저장
        SaveSystem.Instance.SaveGame(saveData);
    }
    
    // 게임 불러오기
    public void LoadGame()
    {
        // 먼저 컴포넌트 참조 확인
        InitializeGameComponents();
        
        if (_playerModel == null)
        {
            Debug.LogError("로드 실패: PlayerModel을 찾을 수 없습니다.");
            return;
        }
        
        // 저장된 데이터 불러오기
        GameSaveData saveData = SaveSystem.Instance.LoadGame();
        
        if (saveData != null)
        {
            // 플레이어 상태 복원 (현재 값에서 차이만큼 업데이트)
            _playerModel.UpdatePlayerSubscriber(saveData.subscriber - _playerModel.Subscriber);
            _playerModel.UpdatePlayerStress(saveData.stress - _playerModel.Stress);
            _playerModel.UpdatePlayerFamous(saveData.famous - _playerModel.Famous);
            
            // 히든 스토리 상태 복원
            RestoreHiddenStories(saveData.discoveredHiddenStories);
            
            // 알림 상태 복원
            RestoreNotifications(saveData.shownNotifications);
            
            Debug.Log("게임 데이터 로드 완료!");
        }
    }
    
    // 플레이어 데이터 초기화
    private void ResetPlayerData()
    {
        if (_playerModel != null)
        {
            // 현재 값에서 초기값으로 변경하는 델타 계산
            _playerModel.UpdatePlayerSubscriber(_initialSubscriber - _playerModel.Subscriber);
            _playerModel.UpdatePlayerStress(_initialStress - _playerModel.Stress);
            _playerModel.UpdatePlayerFamous(_initialFamous - _playerModel.Famous);
        }
        
        // 히든 스토리 발견 기록 초기화
        if (_storySelector != null)
        {
            _storySelector.ResetDiscoveredHiddenStories();
        }
        
        // 알림 기록 초기화
        if (_notificationSystem != null)
        {
            _notificationSystem.ResetNotificationHistory();
        }
    }
    
    // 현재 스토리 티어 가져오기
    private StorySelector.StoryTierType GetCurrentStoryTier()
    {
        if (_storySelector != null)
        {
            return _storySelector.GetStoryTierType(_playerModel.Subscriber);
        }
        return StorySelector.StoryTierType.One; // 기본값
    }
    
    // 발견한 히든 스토리 목록 가져오기
    private List<string> GetDiscoveredHiddenStories()
    {
        List<string> discoveredStories = new List<string>();
        
        if (_storySelector != null)
        {
            // 발견한 히든 스토리들 열거
            foreach (StorySelector.StoryTierType type in System.Enum.GetValues(typeof(StorySelector.StoryTierType)))
            {
                if (type.ToString().StartsWith("Hidden_") && _storySelector.IsHiddenStoryDiscovered(type))
                {
                    discoveredStories.Add(type.ToString());
                }
            }
        }
        
        return discoveredStories;
    }
    
    // 표시된 알림 목록 가져오기
    private List<string> GetShownNotifications()
    {
        List<string> shownNotifications = new List<string>();
        
        if (_notificationSystem != null)
        {
            // NotificationSystem에서 표시된 알림 목록을 가져오는 로직
            // 실제 구현은 NotificationSystem 내부 구조에 따라 다를 수 있음
            // 예: _notificationSystem.GetShownNotifications()
        }
        
        return shownNotifications;
    }
    
    // 히든 스토리 상태 복원
    private void RestoreHiddenStories(List<string> discoveredHiddenStories)
    {
        if (_storySelector != null && discoveredHiddenStories != null)
        {
            foreach (string storyTypeName in discoveredHiddenStories)
            {
                _storySelector.ForceActivateHiddenStory(storyTypeName);
            }
        }
    }
    
    // 알림 상태 복원
    private void RestoreNotifications(List<string> shownNotifications)
    {
        if (_notificationSystem != null && shownNotifications != null)
        {
            // NotificationSystem에 표시된 알림 목록을 설정하는 로직
            // 실제 구현은 NotificationSystem 내부 구조에 따라 다를 수 있음
            // 예: _notificationSystem.RestoreNotifications(shownNotifications)
        }
    }
    
    // 앱 종료 시 자동 저장
    private void OnApplicationQuit()
    {
        if (_enableAutoSave)
        {
            SaveGame();
        }
    }
    
    // 앱 일시 정지 시 (백그라운드로 갈 때) 자동 저장
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && _enableAutoSave)
        {
            SaveGame();
        }
    }
}