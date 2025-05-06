using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceView : ViewBase
{
    public enum Tmps
    {
        ChoiceText
    }
    public enum Images
    {
        ChoiceImage
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
