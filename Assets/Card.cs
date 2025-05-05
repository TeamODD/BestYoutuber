using UnityEngine;
using System.Collections;

public class CardDragRotator : MonoBehaviour
{
    [SerializeField] private Transform cardTransform; // 회전시킬 카드
    [SerializeField] private float maxRotation = 30f; // 최대 회전 각도
    [SerializeField] private float maxMoveX = 2f; // 카드가 따라올 수 있는 최대 X 이동 거리 (단위: world)
    [SerializeField] private float maxMoveY = 2f; // 카드가 따라올 수 있는 최대 Y 이동 거리

    private Vector2 screenCenter;
    private Vector3 originalPosition;
    private bool isDragging = false;
    private Coroutine resetCoroutine;

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        originalPosition = cardTransform.position;
    }

    void Update()
    {
        Vector2 inputPos = Vector2.zero;
        bool isTouching = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPos = touch.position;
            isTouching = true;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchStart(inputPos);
                    break;
                case TouchPhase.Moved:
                    OnTouchMove(inputPos);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnTouchEnd();
                    break;
            }
        }
        else if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            inputPos = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                OnTouchStart(inputPos);
            }
            else if (Input.GetMouseButton(0))
            {
                OnTouchMove(inputPos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnTouchEnd();
            }
        }
    }

    void OnTouchStart(Vector2 pos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == cardTransform.gameObject)
        {
            isDragging = true;

            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
            }
        }
    }

    void OnTouchMove(Vector2 pos)
    {
        if (!isDragging) return;

        Vector2 dragOffset = pos - screenCenter;

        // 회전
        float xNormalized = Mathf.Clamp(dragOffset.x / (Screen.width / 2f), -1f, 1f);
        float rotationZ = -xNormalized * maxRotation;
        cardTransform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        // 이동 - 화면 좌표를 월드 좌표로 변환 후 중심 기준 거리 제한
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        worldPos.z = 0f;

        Vector3 offset = worldPos - Camera.main.ScreenToWorldPoint(screenCenter);
        offset.x = Mathf.Clamp(offset.x, -maxMoveX, maxMoveX);
        offset.y = Mathf.Clamp(offset.y, -maxMoveY, maxMoveY);

        cardTransform.position = originalPosition + offset;
    }

    void OnTouchEnd()
    {
        if (!isDragging) return;
        isDragging = false;

        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }
        resetCoroutine = StartCoroutine(ResetCard());
    }

    IEnumerator ResetCard()
    {
        Quaternion startRotation = cardTransform.rotation;
        Quaternion targetRotation = Quaternion.identity;

        Vector3 startPos = cardTransform.position;
        Vector3 targetPos = originalPosition;

        float duration = 0.1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            cardTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            cardTransform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        cardTransform.rotation = targetRotation;
        cardTransform.position = targetPos;
    }
}
