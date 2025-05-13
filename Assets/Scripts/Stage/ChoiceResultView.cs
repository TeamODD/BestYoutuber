using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceResultView : ViewBase
{
    public enum Tmps
    {
        LeftResultCommentText,
        RightResultCommentText,
    }

    public enum Images
    {
        LeftSelectChangeImage,
        RightSelectChangeImage,
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
