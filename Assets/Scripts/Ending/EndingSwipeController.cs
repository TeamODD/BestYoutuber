using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSwipeController : MonoBehaviour
{
    [SerializeField] private RectTransform _endingImage;
    [SerializeField] private float _swipeThreshold = 100f;
    [SerializeField] private float _swipeSpeed = 5f;
    [SerializeField] private string nextSceneName = "MainMenu";

    private Vector2 _startPos;
    private bool _isDragging = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
            _isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            Vector2 endPos = Input.mousePosition;
            float deltaX = endPos.x - _startPos.x;

            if (Mathf.Abs(deltaX) > _swipeThreshold)
            {
                float direction = Mathf.Sign(deltaX);
                Vector3 target = _endingImage.anchoredPosition + new Vector2(direction * 1000, 0);
                StartCoroutine(SwipeAndLoad(target));
            }

            _isDragging = false;
        }
    }

    private System.Collections.IEnumerator SwipeAndLoad(Vector3 target)
    {
        float time = 0;
        Vector3 start = _endingImage.anchoredPosition;

        while (time < 1)
        {
            _endingImage.anchoredPosition = Vector3.Lerp(start, target, time);
            time += Time.deltaTime * _swipeSpeed;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
