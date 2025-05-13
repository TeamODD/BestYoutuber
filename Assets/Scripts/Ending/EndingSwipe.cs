using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndingSwipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private bool _isDragging = false;
    private float _targetZRotation = 0f;
    private float _rotationAmount = 25f;
    private float _sensitivity = 0.2f;
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
        if (_isDragging)
        {
            Vector2 currentPosition = GetInputPosition();
            float delta = (currentPosition.x - _lastTouchPosition.x) / Screen.width;

            // Z축 회전만 적용
            _targetZRotation = Mathf.Clamp(
                _targetZRotation - delta * _rotationAmount * _sensitivity * 100f,
                -_rotationAmount, _rotationAmount);

            _lastTouchPosition = currentPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        if (_targetZRotation > 20f)
        {
            SceneManager.LoadScene(SceneMainMenu);
        }
        else if (_targetZRotation < -20f)
        {
            SceneManager.LoadScene(SceneGame);
        }

        // 원위치로 복귀
        _targetZRotation = 0f;
    }

    private void Update()
    {
        float currentZ = _rectTransform.localEulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;

        float newZ = Mathf.Lerp(currentZ, _targetZRotation, Time.deltaTime * 10f);

        _rectTransform.localEulerAngles = new Vector3(0, 0, newZ);
    }

    private Vector2 GetInputPosition()
    {
        return Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
    }
}
