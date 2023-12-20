using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeEffect : MonoBehaviour
{
    private float fadeTime = 1.25f;

    [SerializeField]
    private Image LightImage;
    [SerializeField]
    private Image AvoidImage;
    [SerializeField]
    private Image DarknessImage;

    private void Start()
    {

        StartCoroutine(FadeInOutText());
    }
    IEnumerator FadeInOutText()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0.1f));
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(Fade(0.1f, 1));
            yield return new WaitForSeconds(0.1f);
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

            Color lightcolor = LightImage.color;
            Color darknesscolor = DarknessImage.color;
            Color avoidcolor = AvoidImage.color;
            lightcolor.a = Mathf.Lerp(start, end, percent);
            darknesscolor.a = Mathf.Lerp(end, start, percent);
            avoidcolor = new Color(Mathf.Lerp(0.5f + start / 2, 0.5f + end / 2, percent), Mathf.Lerp(0.5f + start / 2, 0.5f + end / 2, percent), Mathf.Lerp(0.5f + start / 2, 0.5f + end / 2, percent));
            LightImage.color = lightcolor;
            DarknessImage.color = darknesscolor;
            AvoidImage.color = avoidcolor;

            yield return null;
        }
    }
}
