using UnityEngine;

public class LogoUpDown : MonoBehaviour
{
    public float moveDistance = 1f; 
    public float moveSpeed = 2f;    
    private Vector3 startPos;       

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance, startPos.z);
    }
}
