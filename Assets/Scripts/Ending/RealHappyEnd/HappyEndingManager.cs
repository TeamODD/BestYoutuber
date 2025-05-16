using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HappyEndingManager : MonoBehaviour
{
    [Header("이미지 순차 출력")]
    [SerializeField] private Image firstImage;
    [SerializeField] private Image secondImage;
    [SerializeField] private float delayBetweenImages = 2f;

    [Header("크레딧")]
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

        // 이동 목표: 크레딧 전체가 화면 아래를 벗어날 만큼
        float creditHeight = creditGroup.rect.height;
        float screenHeight = ((RectTransform)creditGroup.parent).rect.height;
        targetY = startPosition.y + creditHeight + screenHeight;

        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        // 첫 이미지
        firstImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenImages);

        // 두 번째 이미지
        firstImage.gameObject.SetActive(false);
        secondImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBetweenImages);

        // 이미지 숨기고 크레딧 시작
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
