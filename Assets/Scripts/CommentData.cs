using UnityEngine;

[System.Serializable]
public class CommentData
{
    [SerializeField] private int _successChance;

    [SerializeField] private string _choiceComment;

    [SerializeField] private string _successComment;
    [SerializeField] private string _successDescription;

    [SerializeField] private string _failComment;
    [SerializeField] private string _failDescription;

    [SerializeField] private int _successFamousIncrease;
    [SerializeField] private int _successStressDecrease;
    [SerializeField] private int _successSubscriberIncrease;

    [SerializeField] private int _failFamousIncrease;
    [SerializeField] private int _failStressDecrease;
    [SerializeField] private int _failSubscriberIncrease;

    public int SuccessChance => _successChance;
    public string ChoiceComment => _choiceComment;

    public string SuccessComment => _successComment;
    public string SuccessDescription => _successDescription;

    public string FailComment => _failComment;
    public string FailDescription => _failDescription;

    public int SuccessFamousIncrease => _successFamousIncrease;
    public int SuccessStressDecrease => _successStressDecrease;
    public int SuccessSubscriberIncrease => _successSubscriberIncrease;

    public int FailFamousIncrease => _failFamousIncrease;
    public int FailStressDecrease => _failStressDecrease;
    public int FailSubscriberIncrease => _failSubscriberIncrease;

}
