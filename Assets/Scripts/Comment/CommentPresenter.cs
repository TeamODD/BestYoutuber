using UnityEngine;

public enum CommentType
{
    Sun, Ak, None
}

public class CommentPresenter : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private CharacterCommentData[] _comments;
    [SerializeField] private GameObject _commentPrefab;

    [SerializeField] private Transform _contentTransform;

    public void CreateNewComment()
    {
        int random = Random.Range(0, _comments.Length);

        GameObject clone = Instantiate(_commentPrefab, _contentTransform);
        CommentModel commentModel = clone.GetComponent<CommentModel>();
        commentModel.Initialize(_playerModel, _comments[random].CommentType);

        CommentView commentView = clone.GetComponent<CommentView>();
        commentView.SetCommentText(_comments[random].CommentText);
        commentView.OnExitButtonClicked += commentModel.UpdatePlayerStateByCommentType;
    }
}
