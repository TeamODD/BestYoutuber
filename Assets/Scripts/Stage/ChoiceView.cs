using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceView : ViewBase
{
    private float _textSpeed = 0.05f;
    private Coroutine _curCor;

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

    public void StartTypeLineCor(TextMeshProUGUI tmp, string text)
    {
        if(_curCor != null)
            StopAllCoroutines();
        tmp.text = string.Empty;

        _curCor = StartCoroutine(TypeLine(tmp, text));
    }

    private IEnumerator TypeLine(TextMeshProUGUI tmp, string text)
    {
        foreach (char c in text.ToCharArray())
        {
            tmp.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}
