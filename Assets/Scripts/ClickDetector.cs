using Managers;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private GameObject _choiceChildGroup;

    [SerializeField] private GameObject _leftSelectChanceGroup;
    [SerializeField] private GameObject _rightSelectChanceGroup;

    [SerializeField] private GameObject _leftChoiceResultGroup;
    [SerializeField] private GameObject _rightChoiceResultGroup;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked");
            _choiceChildGroup.SetActive(true);

            _leftChoiceResultGroup.SetActive(false);
            _rightChoiceResultGroup.SetActive(false);

            StageManager.instance.SetNewStory();
        }
    }
}
