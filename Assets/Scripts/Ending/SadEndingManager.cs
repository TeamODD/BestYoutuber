using UnityEngine;
using UnityEngine.SceneManagement;

public class SadEndingManager : MonoBehaviour
{
    public void ShowSadEnding(SadEndingData data)
    {
        Debug.Log($"[SadEndingManager] 엔딩 조건 충족: {data.EndingType}");

        SadEndingDataProvider.CurrentData = data;

        if (!string.IsNullOrEmpty(data.TargetSceneName))
        {
            SceneManager.LoadScene(data.TargetSceneName);
        }
        else
        {
            Debug.LogError("TargetSceneName이 비어 있습니다.");
        }
    }

}
