using UnityEngine;
using UnityEngine.SceneManagement;

public class SadEndingManager : MonoBehaviour
{
    public void ShowSadEnding(SadEndingData data)
    {
        Debug.Log($"[SadEndingManager] ���� ���� ����: {data.EndingType}");

        SadEndingDataProvider.CurrentData = data;

        if (!string.IsNullOrEmpty(data.TargetSceneName))
        {
            SceneManager.LoadScene(data.TargetSceneName);
        }
        else
        {
            Debug.LogError("TargetSceneName�� ��� �ֽ��ϴ�.");
        }
    }

}
