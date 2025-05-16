using UnityEngine;

public class LogoUpDown : MonoBehaviour
{
    [SerializeField]
    private float moveDistance = 1f;
    [SerializeField]
    private float moveSpeed = 2f;    
    private Vector3 startPos;       

    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance, startPos.z);
    }
}
