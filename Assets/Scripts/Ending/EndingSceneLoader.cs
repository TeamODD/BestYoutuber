using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneLoader : MonoBehaviour
{
    [Header("���� ������")]
    public SadEndingData endingData;

    [Header("UI ����")]
    [SerializeField] private Image endingImage;
    [SerializeField] private TextMeshProUGUI endingDescription;

    private void Start()
    {
        if (endingData != null)
        {
            endingImage.sprite = endingData.EndingImage;
            endingDescription.text = endingData.Description;
            Debug.Log($"[EndingSceneLoader] '{endingData.EndingType}' ���� ������ �ε��");
        }
        else
        {
            Debug.LogError("[EndingSceneLoader] SadEndingData�� ������� �ʾҽ��ϴ�!");
        }
    }
}
