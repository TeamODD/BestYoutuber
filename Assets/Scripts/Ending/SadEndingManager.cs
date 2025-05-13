using UnityEngine;
using UnityEngine.SceneManagement;

public class SadEndingManager : MonoBehaviour
{
    public void ShowSadEnding(SadEndingData data)
    {
        Debug.Log($"[SadEndingManager] ���� ����: {data.EndingType}");

        if (!string.IsNullOrEmpty(data.TargetSceneName))
        {
            SceneManager.LoadScene(data.TargetSceneName);
        }
        else
        {
            Debug.LogError("TargetSceneName �� ��� �ֽ��ϴ�!");
        }
    }

    public void HandleSwipeResult(bool isLeft)
    {
        if (isLeft)
        {
            Debug.Log("[SadEndingManager] ���� �������� �� MainMenu�� �̵�");
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("[SadEndingManager] ������ �������� �� ���� �� �����");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


