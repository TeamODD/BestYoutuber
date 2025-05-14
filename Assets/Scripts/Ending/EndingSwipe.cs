using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndingSwipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private bool _isDragging = false;
    private float _targetZRotation = 0f;

    [SerializeField] private float _rotationAmount = 25f;       // 최대 회전 각도
    [SerializeField] private float _sensitivity = 0.2f;          // 드래그 민감도
    [SerializeField] private float _rotationSpeed = 10f;         // 회전 보간 속도

    private Vector2 _lastTouchPosition;

    private const string SceneMainMenu = "MainMenu";
    private const string SceneGame = "GameScenePr";

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _lastTouchPosition = GetInputPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        Vector2 currentPosition = GetInputPosition();
        float delta = (currentPosition.x - _lastTouchPosition.x) / Screen.width;

        _targetZRotation = Mathf.Clamp(
            _targetZRotation - delta * _rotationAmount * _sensitivity * 100f,
            -_rotationAmount, _rotationAmount);

        _lastTouchPosition = currentPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        // 회전 각도 기준으로 판단
        if (_targetZRotation > 20f)
        {
            SceneManager.LoadScene(SceneMainMenu);
        }
        else if (_targetZRotation < -20f)
        {
            SceneManager.LoadScene(SceneGame);
        }

        // 회전 초기화 시작
        _targetZRotation = 0f;
    }

    private void Update()
    {
        float currentZ = _rectTransform.localEulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;

        float newZ = Mathf.Lerp(currentZ, _targetZRotation, Time.deltaTime * _rotationSpeed);
        _rectTransform.localEulerAngles = new Vector3(0f, 0f, newZ);
    }

    private Vector2 GetInputPosition()
    {
        return Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
    }
}
