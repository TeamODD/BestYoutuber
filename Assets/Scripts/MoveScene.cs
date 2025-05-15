using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName = "GameScenePr";
    [SerializeField] private float delayInSeconds = 3f;
    [SerializeField] private bool debugModeEnabled = true;

    void Start()
    {
        if (debugModeEnabled)
        {
            Debug.Log($"[MoveScene] 디버깅 모드 활성화: {delayInSeconds}초 후 '{targetSceneName}' 씬으로 이동합니다.");
            StartCoroutine(MoveToSceneAfterDelay());
        }
    }

    private IEnumerator MoveToSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        
        // 씬이 빌드 설정에 포함되어 있는지 확인
        if (SceneUtility.GetBuildIndexByScenePath(targetSceneName) != -1 || 
            SceneUtility.GetBuildIndexByScenePath("Scenes/" + targetSceneName) != -1)
        {
            Debug.Log($"[MoveScene] '{targetSceneName}' 씬으로 이동합니다.");
            
            // 씬 전환에 SceneLoader를 사용할 경우
            if (Managers.SceneLoader.Instance != null)
            {
                Managers.SceneLoader.LoadScene(targetSceneName);
            }
            else
            {
                // 일반 씬 로드 사용
                SceneManager.LoadScene(targetSceneName);
            }
        }
        else
        {
            Debug.LogError($"[MoveScene] 오류: '{targetSceneName}' 씬을 찾을 수 없습니다. 빌드 설정에 씬이 추가되어 있는지 확인하세요.");
        }
    }

    // 인스펙터에서 직접 씬 이동을 트리거할 수 있는 메서드
    public void MoveToTargetSceneImmediately()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToSceneAfterDelay());
    }
}