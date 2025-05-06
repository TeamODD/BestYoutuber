using UnityEngine;

[System.Serializable]
public class StoryData
{
    [SerializeField] private int _index;
    [SerializeField] private string _choiceText;
    [SerializeField] private Sprite _choiceSprite;
    [SerializeField] private CommentData _failCommentModel;
    [SerializeField] private CommentData _successCommentModel;

    public int Index => _index;
    public string ChoiceText => _choiceText;
    public Sprite ChoiceSprite => _choiceSprite;
    public CommentData FailCommentModel => _failCommentModel;
    public CommentData SuccessCommentModel => _successCommentModel;
}
