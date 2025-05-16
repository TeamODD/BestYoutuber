using UnityEngine;

public class CommentModel : MonoBehaviour
{
    private PlayerModel _playerModel;
    private CommentType _commentType;

    public void Initialize(PlayerModel playerModel, CommentType commentType)
    {
        _playerModel = playerModel;
        _commentType = commentType;
    }

    public void UpdatePlayerStateByCommentType()
    {
        switch(_commentType)
        {
            case CommentType.Sun:
                _playerModel.UpdatePlayerStress(1);
                break;
            case CommentType.Ak:
                _playerModel.UpdatePlayerStress(-1);
                break;
            default:
                break;
        }

        Destroy(this.gameObject);
    }
}
