using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelectView : ViewBase
{
    public enum Tmps
    {
        LeftSelectText,
        RightSelectText,
        LeftSelectChanceText,
        RightSelectChanceText,
    }

    public enum Images
    {
        LeftSelectChanceImage,
        RightSelectChanceImage,
    }
    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
