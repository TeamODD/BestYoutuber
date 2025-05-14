using TMPro;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ResultView : ViewBase
{
    public enum Tmps
    {
        TitleText,
        ResultName1Text,
        ResultName2Text,
        ResultName3Text,
        ReturnText,

        ResultValue1Text,
        ResultValue2Text,
        ResultValue3Text,
    }
    public enum Buttons
    {
        ReturnButton
    }

    public event Action OnPanelClicked;

    [SerializeField] private float _tmpAnimDuration = 0.5f;
    [SerializeField] private float _animSpan = 0.5f;

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Button>(typeof(Buttons));

        Button panelButton = GetButton((int)Buttons.ReturnButton);
        if (panelButton != null)
        {
            panelButton.onClick.AddListener(() =>
            {
                OnPanelClicked?.Invoke();
            });
        }
    }

    public void ShowResult(int testData1, int testData2, int testData3)
    {
        StartCoroutine(ShowResultCo(testData1, testData2, testData3));
    }

    IEnumerator ShowResultCo(int testData1, int testData2, int testData3)
    {
        SetTmpText((int)Tmps.ResultValue1Text, "0");
        SetTmpText((int)Tmps.ResultValue2Text, "0");
        SetTmpText((int)Tmps.ResultValue3Text, "0");

        WaitForSeconds animWait = new WaitForSeconds(_animSpan);

        TextMeshProUGUI tmp = GetTmp((int)Tmps.ResultValue1Text);
        yield return TmpAnim(tmp, testData1);
        yield return animWait;

        tmp = GetTmp((int)Tmps.ResultValue2Text);
        yield return TmpAnim(tmp, testData2);
        yield return animWait;

        tmp = GetTmp((int)Tmps.ResultValue3Text);
        yield return TmpAnim(tmp, testData3);
    }

    IEnumerator TmpAnim(TextMeshProUGUI tmp, int number)
    {
        float elapsed = 0.0f;
        int startNumber = 0;

        while (elapsed < _tmpAnimDuration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / _tmpAnimDuration;
            int currentNumber = (int)Mathf.Lerp(startNumber, number, normalizedTime);
            tmp.text = currentNumber.ToString();
            yield return null;
        }
        tmp.text = number.ToString();
    }
}