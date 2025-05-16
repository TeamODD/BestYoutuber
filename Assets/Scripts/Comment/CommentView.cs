using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CommentView : ViewBase
{
    [SerializeField] private TextMeshProUGUI _commentText;
    public enum Buttons
    {
        ExitButton,
    }

    public event Action OnExitButtonClicked;

    private void Awake()
    {
        //Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Button>(typeof(Buttons));
    }

    private void Start()
    {
        Button commentActiveButton = GetButton((int)Buttons.ExitButton);
        if (commentActiveButton != null)
        {
            commentActiveButton.onClick.AddListener(() =>
            {
                OnExitButtonClicked?.Invoke();
            });
        }
    }

    public void SetCommentText(string text)
    {
        _commentText.text = text;
    }
}