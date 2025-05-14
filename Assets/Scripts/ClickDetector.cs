using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _choiceChildGroup;

    [SerializeField] private GameObject _leftSelectChanceGroup;
    [SerializeField] private GameObject _rightSelectChanceGroup;

    [SerializeField] private GameObject _leftChoiceResultGroup;
    [SerializeField] private GameObject _rightChoiceResultGroup;

    private bool _isPointerOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false;
    }

    void Update()
    {
        if (_isPointerOver && Input.GetMouseButtonDown(0))
        {
            _choiceChildGroup.SetActive(true);

            _leftChoiceResultGroup.SetActive(false);
            _rightChoiceResultGroup.SetActive(false);

            StageManager.instance.SetNewStory();
        }
    }
}
