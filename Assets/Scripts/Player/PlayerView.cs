using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : ViewBase
{
    public enum Images
    {
        StressImage,
        FamousImage
    }
    
    public enum Tmps
    {
        SubscriberText,
        StressText        //스트레스 상태를 텍스트로 보여줌
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
