using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StateView : ViewBase
{
    public enum Images
    {
        CharacterStateImage
    }

    public enum Tmps
    {
        CharacterStateText, CommentWatchText,
    }

    public enum Buttons
    {
        CommentActiveButton,
    }

    public event Action OnCommentActiveButtonClicked;
    public event Action OnDisabled;

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
    }
    private void OnDisable()
    {
        OnDisabled?.Invoke();
    }

    private void Start()
    {
        Button commentActiveButton = GetButton((int)Buttons.CommentActiveButton);
        if (commentActiveButton != null)
        {
            commentActiveButton.onClick.AddListener(() => {
                OnCommentActiveButtonClicked?.Invoke();
            });
        }
    }
}
