using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPbar : MonoBehaviour
{
    private float hp = 100f;
    private float maxHP = 100f;

    public Image fill;
    public TMP_Text filltext;

    private Color blueColor = new Color(0.10f, 0.23f, 0.34f);
    private Color greenColor = new Color(0.18f, 0.55f, 0.25f);
    private Color originalColor;

    void Start()
    {
        fill.fillAmount = hp / maxHP;
        filltext.text = $"{(int)hp}";
        originalColor = fill.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp -= 10f;
            hp = Mathf.Max(hp, 0f);
            ChangeColor(blueColor);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            hp += 10f;
            hp = Mathf.Min(hp, maxHP);
            ChangeColor(greenColor);
        }

        fill.fillAmount = hp / maxHP;
        filltext.text = $"{(int)hp}";
    }

    void ChangeColor(Color targetColor)
    {
        StartCoroutine(ChangeColorCoroutine(targetColor));
    }

    private IEnumerator ChangeColorCoroutine(Color targetColor)
    {
        fill.color = targetColor;
        yield return new WaitForSeconds(0.3f);
        fill.color = originalColor;
    }
}
