using UnityEngine;

[CreateAssetMenu(fileName = "StoryData", menuName = "Story/StoryData")]
public class StoryData : ScriptableObject
{
    [SerializeField] private string _choiceText;
    [SerializeField] private Sprite _choiceSprite;
    [SerializeField] private CommentData _rightCommentData;
    [SerializeField] private CommentData _leftCommentData;

    public string ChoiceText => _choiceText;
    public Sprite ChoiceSprite => _choiceSprite;
    public CommentData RightCommentData => _rightCommentData;
    public CommentData LeftCommentData => _leftCommentData;
}
