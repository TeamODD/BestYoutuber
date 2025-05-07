using UnityEngine;
using UnityEngine.EventSystems;

public class FingerDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _leftSelectChanceGroup;
    [SerializeField] private GameObject _rightSelectChanceGroup;

    [SerializeField] private GameObject _leftChoiceResultGroup;
    [SerializeField] private GameObject _rightChoiceResultGroup;

    [SerializeField] private GameObject _choiceChildGroup;

    [SerializeField] private StoryModel _storyModel;

    RectTransform rectTransform;
    bool isDragging = false;
    float targetZRotation = 0f;
    float rotationAmount = 25f; 
    float sensitivity = 0.2f;

    public bool canMove;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //아직 안 씀
    public void Initialize(GameObject leftSelectChanceGroup, GameObject rightSelectChanceGroup, GameObject leftChoiceResultGroup, GameObject rightChoiceResultGroup, GameObject choiceChildGroup
        , StoryModel storyModel)
    {
        _leftChoiceResultGroup = leftChoiceResultGroup;
        _rightChoiceResultGroup = rightChoiceResultGroup;
        _leftSelectChanceGroup = leftSelectChanceGroup;
        _rightSelectChanceGroup = rightSelectChanceGroup;   
        _storyModel = storyModel;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (targetZRotation > 20f)
        {
            _storyModel.UpdatePlayerUI(true);
            _leftChoiceResultGroup.SetActive(true);
            _choiceChildGroup.SetActive(false);
            canMove = false;
        }
        else if(targetZRotation < -20f)
        {
            _storyModel.UpdatePlayerUI(false);
            _rightChoiceResultGroup.SetActive(true);
            _choiceChildGroup.SetActive(false);
            canMove = false;
        }

        targetZRotation = 0f;

        _leftSelectChanceGroup.SetActive(false);
        _rightSelectChanceGroup.SetActive(false);
    }

    void Update()
    {
        if (canMove)
        {
            if (isDragging)
            {
                Vector2 localMousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localMousePos);

                if (localMousePos.x < 0)
                {
                    _leftSelectChanceGroup.SetActive(true);
                    _rightSelectChanceGroup.SetActive(false);
                }
                else if (localMousePos.x > 0)
                {
                    _leftSelectChanceGroup.SetActive(false);
                    _rightSelectChanceGroup.SetActive(true);
                }

                float delta = Input.GetAxis("Mouse X");
                targetZRotation = Mathf.Clamp(targetZRotation - delta * rotationAmount * sensitivity, -rotationAmount, rotationAmount);
            }

            float currentZ = rectTransform.localEulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f;

            float newZ = Mathf.Lerp(currentZ, targetZRotation, Time.deltaTime * 10f);
            rectTransform.localEulerAngles = new Vector3(0, 0, newZ);
        }
    }

}