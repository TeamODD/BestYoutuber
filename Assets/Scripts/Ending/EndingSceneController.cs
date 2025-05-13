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
        Time.timeScale = 1f; // Ȥ�� �������� ��� ���
    }

    void Update()
    {
        if (inputLocked) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(TiltAndLoad(-1)); // ���� ����
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(TiltAndLoad(1)); // ������ ����
        }
    }

    IEnumerator TiltAndLoad(int direction)
    {
        inputLocked = true;

        float duration = 1.0f;
        float time = 0f;

        Quaternion startRotation = endingImage.rectTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, direction * 30f); // ���� ����

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
