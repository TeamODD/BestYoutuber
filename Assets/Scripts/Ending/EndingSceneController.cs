using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndingSceneController : MonoBehaviour
{
    public Image endingImage;
    private bool inputLocked = false;

    void Start()
    {
        Time.timeScale = 1f; // 혹시 멈춰있을 경우 대비
    }

    void Update()
    {
        if (inputLocked) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(TiltAndLoad(-1)); // 왼쪽 기울기
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(TiltAndLoad(1)); // 오른쪽 기울기
        }
    }

    IEnumerator TiltAndLoad(int direction)
    {
        inputLocked = true;

        float duration = 1.0f;
        float time = 0f;

        Quaternion startRotation = endingImage.rectTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, direction * 30f); // 기울기 각도

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            endingImage.rectTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (direction == -1)
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
