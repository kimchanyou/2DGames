using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.5f;

    public void OnMove(float x, float y)
    {
        Vector2 start = transform.position;
        Vector2 end = new Vector2(x, y);

        StartCoroutine(MoveProcess(start, end));
    }
    private IEnumerator MoveProcess(Vector2 start, Vector2 end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector2.Lerp(start, end, percent);

            yield return null;
        }
    }
}
