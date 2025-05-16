using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    [Header("튜토리얼 UI 목록 (순서대로 등장)")]
    [SerializeField] private RectTransform[] uiSequence;
    [SerializeField] private GameObject[] uiObjects;

    [Header("설명 텍스트")]
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private string[] explanations;
    [SerializeField] private Vector2[] explanationOffsets;

    private int _step = 0;
    private bool _tutorialFinished = false;

    private void Start()
    {
        foreach (GameObject obj in uiObjects)
            obj.SetActive(false);

        explanationText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_tutorialFinished)
            {
                SceneManager.LoadScene("GameScenePr");
                return;
            }

            ShowNextStep();
        }
    }

    private void ShowNextStep()
    {
        if (_step >= uiObjects.Length)
        {
            explanationText.gameObject.SetActive(false);
            Debug.Log("튜토리얼 끝");
            SceneManager.LoadScene("GameScenePr"); // 즉시 씬 전환
            return;
        }

        uiObjects[_step].SetActive(true);
        explanationText.text = explanations[_step];

        Vector2 worldPos = (Vector2)uiSequence[_step].position + explanationOffsets[_step];
        explanationText.rectTransform.position = worldPos;
        explanationText.gameObject.SetActive(true);

        _step++;
    }
}
