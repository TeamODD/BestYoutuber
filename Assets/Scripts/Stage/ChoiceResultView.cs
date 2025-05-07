using TMPro;
using UnityEngine;

public class ChoiceResultView : ViewBase
{
    public enum Tmps
    {
        LeftResultCommentText,
        RightResultCommentText,
        LeftResultDescriptionText,
        RightResultDescriptionText
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
    }
}
