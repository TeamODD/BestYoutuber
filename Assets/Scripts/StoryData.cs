using UnityEngine;

[System.Serializable]
public class StoryData
{
    [SerializeField] private int _index;
    [SerializeField] private string _choiceText;
    [SerializeField] private Sprite _choiceSprite;
    [SerializeField] private CommentData _rightCommentData;
    [SerializeField] private CommentData _leftCommentData;

    public int Index => _index;
    public string ChoiceText => _choiceText;
    public Sprite ChoiceSprite => _choiceSprite;
    public CommentData RightCommentData => _rightCommentData;
    public CommentData LeftCommentData => _leftCommentData;
}
