using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailEffect : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float duration = 0.5f;

    public SpriteRenderer spriteRenderer;
    private Color originColor;
    private Vector3 originScale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        originScale = transform.localScale;
        spriteRenderer.flipX = true;
    }

    private void OnEnable()
    {
        spriteRenderer.color = originColor;
        transform.localScale = originScale;

        StartCoroutine("Process");
    }

    private IEnumerator Process()
    {
        float current = 0;
        float percent = 0;
        Vector3 start = transform.localScale * 0.8f;
        Vector3 end = transform.localScale * 0.2f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / duration;

            // 색상의 알파 값을 1(255)에서 0(0)으로 변경
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1, 0, curve.Evaluate(percent));
            spriteRenderer.color = color;

            // 크기를 start(0.8, 0.8, 0.8)에서 end(0.2, 0.2, 0.2)으로 변경
            transform.localScale = Vector3.Lerp(start, end, curve.Evaluate(percent));

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
