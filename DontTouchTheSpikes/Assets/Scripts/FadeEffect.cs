using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 0.5f;
    private TextMeshProUGUI fadeText;

    private void Start()
    {
        fadeText = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadeInOutText());
    }
    IEnumerator FadeInOutText()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));

            yield return StartCoroutine(Fade(0, 1));
        }
    }

    IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = fadeText.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeText.color = color;

            yield return null;
        }
    }
}
