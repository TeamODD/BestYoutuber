using UnityEngine;
using UnityEngine.EventSystems;

public class FingerDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject _leftSelectChanceGroup;
    [SerializeField] private GameObject _rightSelectChanceGroup;
    [SerializeField] private GameObject _leftChoiceResultGroup;
    [SerializeField] private GameObject _rightChoiceResultGroup;
    [SerializeField] private GameObject _choiceChildGroup;
    [SerializeField] private StoryModel _storyModel;
    [SerializeField] private SadEndingManager _endingManager;

    private RectTransform _rectTransform;
    private bool _isDragging = false;
    private float _targetZRotation = 0f;
    private float _rotationAmount = 25f;
    private float _sensitivity = 0.2f;
    public bool canMove;

    private bool _endingSwipeMode = false;
    private Vector2 _lastTouchPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Debug.Log("[FingerDetector] Awake() 호출됨");
    }

    public void Initialize(
        GameObject leftSelectChanceGroup, GameObject rightSelectChanceGroup,
        GameObject leftChoiceResultGroup, GameObject rightChoiceResultGroup,
        GameObject choiceChildGroup, StoryModel storyModel)
    {
        _leftChoiceResultGroup = leftChoiceResultGroup;
        _rightChoiceResultGroup = rightChoiceResultGroup;
        _leftSelectChanceGroup = leftSelectChanceGroup;
        _rightSelectChanceGroup = rightSelectChanceGroup;
        _choiceChildGroup = choiceChildGroup;
        _storyModel = storyModel;
    }

    public void EnableEndingSwipe(bool enable)
    {
        _endingSwipeMode = enable;
        Debug.Log($"[FingerDetector] EnableEndingSwipe({enable}) 호출됨");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _lastTouchPosition = GetInputPosition();
        Debug.Log("[FingerDetector] OnPointerDown - 터치 시작");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove || _endingSwipeMode)
        {
            HandleDragInput();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        Debug.Log($"[FingerDetector] OnPointerUp 호출 - endingSwipeMode = {_endingSwipeMode}, Z = {_targetZRotation}");

        if (_endingSwipeMode)
        {
            if (_targetZRotation > 20f)
            {
                Debug.Log("[FingerDetector] → 왼쪽 스와이프 인식");
                if (_endingManager == null)
                {
                    Debug.LogError("[FingerDetector] _endingManager가 연결되지 않았습니다!");
                }
                else
                {
                    _endingManager.HandleSwipeResult(true);
                }
            }
            else if (_targetZRotation < -20f)
            {
                Debug.Log("[FingerDetector] → 오른쪽 스와이프 인식");
                if (_endingManager == null)
                {
                    Debug.LogError("[FingerDetector] _endingManager가 연결되지 않았습니다!");
                }
                else
                {
                    _endingManager.HandleSwipeResult(false);
                }
            }
            else
            {
                Debug.Log("[FingerDetector] → 회전 각도가 부족하여 스와이프 실패");
            }

            return;
        }

        // 선택지 모드 처리
        if (_targetZRotation > 20f)
        {
            Debug.Log("[FingerDetector] 선택지: 왼쪽 선택 실행");
            _storyModel.UpdatePlayerUI(true);
            _leftChoiceResultGroup?.SetActive(true);
            _choiceChildGroup?.SetActive(false);
            _rectTransform.localEulerAngles = Vector3.zero;
            canMove = false;
        }
        else if (_targetZRotation < -20f)
        {
            Debug.Log("[FingerDetector] 선택지: 오른쪽 선택 실행");
            _storyModel.UpdatePlayerUI(false);
            _rightChoiceResultGroup?.SetActive(true);
            _choiceChildGroup?.SetActive(false);
            _rectTransform.localEulerAngles = Vector3.zero;
            canMove = false;
        }

        _targetZRotation = 0f;

        _leftSelectChanceGroup?.SetActive(false);
        _rightSelectChanceGroup?.SetActive(false);
    }

    private void Update()
    {
        if (canMove || _endingSwipeMode)
        {
            float currentZ = _rectTransform.localEulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f;

            float newZ = Mathf.Lerp(currentZ, _targetZRotation, Time.deltaTime * 10f);
            _rectTransform.localEulerAngles = new Vector3(0, 0, newZ);
        }
    }

    private void HandleDragInput()
    {
        Vector2 currentPosition = GetInputPosition();
        Vector2 localInputPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform, currentPosition, Camera.main, out localInputPos);

        if (!_endingSwipeMode)
        {
            if (localInputPos.x < 0)
            {
                _leftSelectChanceGroup?.SetActive(true);
                _rightSelectChanceGroup?.SetActive(false);
            }
            else if (localInputPos.x > 0)
            {
                _leftSelectChanceGroup?.SetActive(false);
                _rightSelectChanceGroup?.SetActive(true);
            }
        }

        float delta = (currentPosition.x - _lastTouchPosition.x) / Screen.width;

        _targetZRotation = Mathf.Clamp(
            _targetZRotation - delta * _rotationAmount * _sensitivity * 100f,
            -_rotationAmount, _rotationAmount);

        _lastTouchPosition = currentPosition;
    }

    private Vector2 GetInputPosition()
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position;
        else
            return Input.mousePosition;
    }
}
