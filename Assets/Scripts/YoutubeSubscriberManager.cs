using System.Collections;
using UnityEngine;

public class YoutubeSubscriberManager : MonoBehaviour
{
    [SerializeField]
    private PlayerModel _playerModel;
    
    [SerializeField]
    private NotificationSystem _notificationSystem;

    public GameObject SilverButton;
    public GameObject GoldButton;
    
    private bool _silverButtonShown = false;
    private bool _goldButtonShown = false;

    private void Update()
    {
        SubscriberButtonManager();
    }

    private void SubscriberButtonManager()
    {
        if (_playerModel.Subscriber >= 100000 && !_silverButtonShown)
        {
            _silverButtonShown = true;
            StartCoroutine(ShowButtonTemporarily(SilverButton, 3f, "Silver"));
        }
        
        if (_playerModel.Subscriber >= 1000000 && !_goldButtonShown)
        {
            _goldButtonShown = true;
            StartCoroutine(ShowButtonTemporarily(GoldButton, 3f, "Gold"));
        }
    }
    
    private IEnumerator ShowButtonTemporarily(GameObject button, float duration , string buttonType)
    {
        button.SetActive(true);
        if (_notificationSystem != null)
        {
            _notificationSystem.NotifyButtonAchieved(buttonType);
        }
        yield return new WaitForSeconds(duration);
        button.SetActive(false);
    }
}
