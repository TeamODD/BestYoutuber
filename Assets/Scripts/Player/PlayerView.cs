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
        SubscriberText
    }

    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Tmps));
        Bind<Image>(typeof(Images));
    }
}
