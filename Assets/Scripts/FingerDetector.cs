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

    RectTransform _rectTransform;
    bool _isDragging = false;
    float _targetZRotation = 0f;
    float _rotationAmount = 25f;
    float _sensitivity = 0.2f;
    public bool canMove;

    // 터치 입력 처리를 위한 추가 변수
    private Vector2 _lastTouchPosition;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(GameObject leftSelectChanceGroup, GameObject rightSelectChanceGroup,
        GameObject leftChoiceResultGroup, GameObject rightChoiceResultGroup, GameObject choiceChildGroup,
        StoryModel storyModel)
    {
        _leftChoiceResultGroup = leftChoiceResultGroup;
        _rightChoiceResultGroup = rightChoiceResultGroup;
        _leftSelectChanceGroup = leftSelectChanceGroup;
        _rightSelectChanceGroup = rightSelectChanceGroup;
        _storyModel = storyModel;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _lastTouchPosition = GetInputPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove && _isDragging)
        {
            HandleDragInput();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        if (_targetZRotation > 20f)
        {
            _storyModel.UpdatePlayerUI(true);
            _leftChoiceResultGroup.SetActive(true);
            _choiceChildGroup.SetActive(false);
            _rectTransform.localEulerAngles = Vector3.zero;
            canMove = false;
        }
        else if (_targetZRotation < -20f)
        {
            _storyModel.UpdatePlayerUI(false);
            _rightChoiceResultGroup.SetActive(true);
            _choiceChildGroup.SetActive(false);
            _rectTransform.localEulerAngles = Vector3.zero;
            canMove = false;
        }

        _targetZRotation = 0f;

        _leftSelectChanceGroup.SetActive(false);
        _rightSelectChanceGroup.SetActive(false);
    }

    void Update()
    {
        if (canMove)
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

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, currentPosition, Camera.main,
            out localInputPos);

        if (localInputPos.x < 0)
        {
            _leftSelectChanceGroup.SetActive(true);
            _rightSelectChanceGroup.SetActive(false);
        }
        else if (localInputPos.x > 0)
        {
            _leftSelectChanceGroup.SetActive(false);
            _rightSelectChanceGroup.SetActive(true);
        }

        float delta = (currentPosition.x - _lastTouchPosition.x) / Screen.width;

        _targetZRotation = Mathf.Clamp(_targetZRotation - delta * _rotationAmount * _sensitivity * 100f,
            -_rotationAmount, _rotationAmount);

        _lastTouchPosition = currentPosition;
    }

    private Vector2 GetInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }
}