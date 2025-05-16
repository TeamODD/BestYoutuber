using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class EndingSwipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private float rotationAmount = 25f;
    [SerializeField] private float sensitivity = 0.2f;
    [SerializeField] private float rotationSpeed = 10f;

    private RectTransform _rectTransform;
    private bool _isDragging = false;
    private float _targetZRotation = 0f;
    private Vector2 _lastTouchPosition;

    private bool _canSwipe = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // EndingSceneEffect에서 이벤트 받기
        EndingSceneEffect effect = FindObjectOfType<EndingSceneEffect>();
        if (effect != null)
        {
            effect.OnShakeComplete += () =>
            {
                _canSwipe = true;
                Debug.Log("스와이프 가능 상태 진입!");
            };
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_canSwipe) return;

        _isDragging = true;
        _lastTouchPosition = GetInputPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_canSwipe || !_isDragging) return;

        float delta = (GetInputPosition().x - _lastTouchPosition.x) / Screen.width;

        _targetZRotation = Mathf.Clamp(
            _targetZRotation - delta * rotationAmount * sensitivity * 100f,
            -rotationAmount, rotationAmount);

        _lastTouchPosition = GetInputPosition();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_canSwipe) return;

        _isDragging = false;

        if (_targetZRotation > 20f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else if (_targetZRotation < -20f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScenePr");
        }

        _targetZRotation = 0f;
    }

    private void Update()
    {
        float currentZ = _rectTransform.localEulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;

        float newZ = Mathf.Lerp(currentZ, _targetZRotation, Time.deltaTime * rotationSpeed);
        _rectTransform.localEulerAngles = new Vector3(0, 0, newZ);
    }

    private Vector2 GetInputPosition()
    {
        return Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
    }
}
