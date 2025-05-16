using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HappyEndingManager : MonoBehaviour
{
    [Header("�̹��� ���� ���")]
    [SerializeField] private Image firstImage;
    [SerializeField] private Image secondImage;
    [SerializeField] private float delayBetweenImages = 2f;

    [Header("ũ����")]
    [SerializeField] private RectTransform creditGroup;
    [SerializeField] private float creditSpeed = 50f;
    [SerializeField] private float creditStartDelay = 1f;

    private Vector2 startPosition;
    private float targetY;
    private bool startCredit = false;
    private bool hasLoadedMenu = false;

    private void Start()
    {
        startPosition = creditGroup.anchoredPosition;

        // �̵� ��ǥ: ũ���� ��ü�� ȭ�� �Ʒ��� ��� ��ŭ
        float creditHeight = creditGroup.rect.height;
        float screenHeight = ((RectTransform)creditGroup.parent).rect.height;
        targetY = startPosition.y + creditHeight + screenHeight;

        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        // ù �̹���
        firstImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenImages);

        // �� ��° �̹���
        firstImage.gameObject.SetActive(false);
        secondImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenImages);

        // �̹��� ����� ũ���� ����
        secondImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(creditStartDelay);

        startCredit = true;
    }

    private void Update()
    {
        if (startCredit && creditGroup.anchoredPosition.y < targetY)
        {
            creditGroup.anchoredPosition += Vector2.up * creditSpeed * Time.deltaTime;
        }
        else if (startCredit && !hasLoadedMenu)
        {
            hasLoadedMenu = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
