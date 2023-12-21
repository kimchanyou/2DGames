using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lights;

    [SerializeField]
    private float activateX;
    [SerializeField]
    private float deactivateX;

    public int minValue = 2;
    public int maxValue = 6;

    private float moveTime = 0.5f;
    public void ActivateAll()
    {
        int count = Random.Range(minValue, lights.Length - maxValue);

        int[] numerics = RandomNumerics(lights.Length, count);

        OnMove(activateX, transform.position.y - 1.5f);
        for (int i = 0; i < numerics.Length; i++)
        {
            lights[numerics[i]].SetActive(true);
        }
    }
    public void DeactivateAll()
    {
        OnMove(deactivateX, transform.position.y - 1.5f);
        StartCoroutine(CoTurnOff());
    }

    private int[] RandomNumerics(int maxCount, int n)
    {
        // 0 ~ maxCount까지의 숫자 중 겹치지 않는 n개의 난수가 필요할 때 사용
        int[] defaults = new int[maxCount];     // 0 ~ maxCount까지 순서대로 저장하는 배열
        int[] results = new int[n];             // 결과 값들을 저장하는 배열

        // 배열 전체에 0부터 maxCount의 값을 순서대로 저장
        for (int i = 0; i < maxCount; i++)
        {
            defaults[i] = i;
        }

        for (int i = 0; i < n; i++)
        {
            int index = Random.Range(0, maxCount);  // 임의의 숫자를 하나 뽑아서

            results[i] = defaults[index];
            defaults[index] = defaults[maxCount - 1];

            maxCount--;
        }

        return results;
    }


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
    private IEnumerator CoTurnOff()
    {
        yield return new WaitForSeconds(0.55f);
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }
}
