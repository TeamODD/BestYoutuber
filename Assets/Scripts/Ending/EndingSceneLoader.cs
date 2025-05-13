using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneLoader : MonoBehaviour
{
    [Header("엔딩 데이터")]
    public SadEndingData endingData;

    [Header("UI 연결")]
    [SerializeField] private Image endingImage;
    [SerializeField] private TextMeshProUGUI endingDescription;

    private void Start()
    {
        if (endingData != null)
        {
            endingImage.sprite = endingData.EndingImage;
            endingDescription.text = endingData.Description;
            Debug.Log($"[EndingSceneLoader] '{endingData.EndingType}' 엔딩 데이터 로드됨");
        }
        else
        {
            Debug.LogError("[EndingSceneLoader] SadEndingData가 연결되지 않았습니다!");
        }
    }
}
