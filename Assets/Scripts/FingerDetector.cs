using UnityEngine;
using UnityEngine.EventSystems;

public class FingerDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject leftSelectChanceGroup;
    [SerializeField] private GameObject rightSelectChanceGroup;

    [SerializeField] private GameObject leftChoiceResultGroup;
    [SerializeField] private GameObject rightChoiceResultGroup;

    [SerializeField] private GameObject choiceChildGroup;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (targetZRotation > 20f)
        {
            leftChoiceResultGroup.SetActive(true);
            choiceChildGroup.SetActive(false);
            canMove = false;
        }
        else if(targetZRotation < -20f)
        {
            rightChoiceResultGroup.SetActive(true);
            choiceChildGroup.SetActive(false);
            canMove = false;
        }

        targetZRotation = 0f;

        leftSelectChanceGroup.SetActive(false);
        rightSelectChanceGroup.SetActive(false);
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
                    leftSelectChanceGroup.SetActive(true);
                    rightSelectChanceGroup.SetActive(false);
                }
                else if (localMousePos.x > 0)
                {
                    leftSelectChanceGroup.SetActive(false);
                    rightSelectChanceGroup.SetActive(true);
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