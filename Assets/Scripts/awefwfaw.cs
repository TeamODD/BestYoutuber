using UnityEngine;

public class awefwfaw : MonoBehaviour
{
    public GameObject one;
    public GameObject two;

    private float _timer = 0f;
    private bool _toggleState = true;

    private void Start()
    {
        // 초기 상태 설정
        if (one != null) one.SetActive(true);
        if (two != null) two.SetActive(false);
    }

    private void Update()
    {
        // 타이머 증가
        _timer += Time.deltaTime;
        
        // 1초마다 상태 전환
        if (_timer >= 1f)
        {
            _timer = 0f; // 타이머 리셋
            _toggleState = !_toggleState; // 상태 반전
            
            // GameObject 활성화 상태 변경
            if (one != null) one.SetActive(_toggleState);
            if (two != null) two.SetActive(!_toggleState);
        }
    }
}