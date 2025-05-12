using UnityEngine;

public class BGM1 : MonoBehaviour
{
    private static BGM1 instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}