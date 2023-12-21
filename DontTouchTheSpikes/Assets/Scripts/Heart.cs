using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float originY;

    private void OnEnable()
    {
        StartCoroutine(CoIdle());
    }

    private IEnumerator CoIdle()
    {
        float deltaY = 0.2f;
        float moveSpeedY = 4.5f;

        while (true)
        {
            float y = originY + deltaY * Mathf.Sin(Time.time * moveSpeedY);
            transform.position = new Vector2(transform.position.x, y);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(CoIdle());
            gameObject.SetActive(false);
        }
    }
}
