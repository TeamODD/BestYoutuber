using System;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "game_save.json";
    private static SaveSystem _instance;
    
    public static SaveSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveSystem>();
                if (_instance == null)
                {
                    GameObject saveSystemObj = new GameObject("SaveSystem");
                    _instance = saveSystemObj.AddComponent<SaveSystem>();
                    DontDestroyOnLoad(saveSystemObj);
                }
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // 게임 데이터 저장
    public void SaveGame(GameSaveData data)
    {
        try
        {
            // 현재 시간 저장
            data.saveTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            
            // JSON으로 직렬화
            string json = JsonUtility.ToJson(data, true);
            
            // 저장 경로 결정
            string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            
            // 파일에 쓰기
            File.WriteAllText(savePath, json);
            
            Debug.Log($"게임 저장 완료: {savePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"게임 저장 실패: {e.Message}");
        }
    }
    
    // 게임 데이터 로드
    public GameSaveData LoadGame()
    {
        try
        {
            string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            
            // 저장 파일이 존재하지 않는 경우
            if (!File.Exists(savePath))
            {
                Debug.Log("저장 파일이 없습니다. 새 게임을 시작합니다.");
                return null;
            }
            
            // 파일에서 읽기
            string json = File.ReadAllText(savePath);
            
            // JSON 역직렬화
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
            
            Debug.Log($"게임 로드 완료: {savePath}");
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"게임 로드 실패: {e.Message}");
            return null;
        }
    }
    
    // 저장 파일 삭제 (새 게임 시작용)
    public void DeleteSave()
    {
        try
        {
            string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                Debug.Log("저장 파일 삭제 완료");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"저장 파일 삭제 실패: {e.Message}");
        }
    }
    
    // 저장 파일 존재 여부 확인
    public bool HasSaveFile()
    {
        string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        return File.Exists(savePath);
    }
}