using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneLoader : MonoBehaviour
{
    [SerializeField] private EndingConditionManager _endingManager;

    public void LoadEndingScene()
    {
        var ending = _endingManager.GetEnding();
        string sceneName = "";

        switch (ending)
        {
            case EndingConditionManager.EndingType.Happy:
                sceneName = "Ending_Happy";
                break;
            case EndingConditionManager.EndingType.Sad1:
                sceneName = "Ending_Sad1";
                break;
            case EndingConditionManager.EndingType.Sad2:
                sceneName = "Ending_Sad2";
                break;
            case EndingConditionManager.EndingType.Sad3:
                sceneName = "Ending_Sad3";
                break;
            case EndingConditionManager.EndingType.Sad4:
                sceneName = "Ending_Sad4";
                break;
        }

        SceneManager.LoadScene(sceneName);
    }
}
