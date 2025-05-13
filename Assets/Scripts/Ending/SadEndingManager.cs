using UnityEngine;
using UnityEngine.SceneManagement;

public class SadEndingManager : MonoBehaviour
{
    public void ShowSadEnding(SadEndingData data)
    {
        Debug.Log($"[SadEndingManager] 조건 충족: {data.EndingType}");

        if (!string.IsNullOrEmpty(data.TargetSceneName))
        {
            SceneManager.LoadScene(data.TargetSceneName);
        }
        else
        {
            Debug.LogError("TargetSceneName 이 비어 있습니다!");
        }
    }

    public void HandleSwipeResult(bool isLeft)
    {
        if (isLeft)
        {
            Debug.Log("[SadEndingManager] 왼쪽 스와이프 → MainMenu로 이동");
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("[SadEndingManager] 오른쪽 스와이프 → 현재 씬 재시작");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


