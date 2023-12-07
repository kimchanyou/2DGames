using System.Collections;
using UnityEngine;


public class SpikeSpawner : MonoBehaviour
{
    [SerializeField]
    private Spike[] spikes;
    [SerializeField]
    private float activateX;
    [SerializeField]
    private float deactivateX;

    public void ActivateAll()
    {
        int count = Random.Range(1, spikes.Length);

        int[] numerics = RandomNumerics(spikes.Length, count);

        for(int i = 0; i < numerics.Length; i++)
        {
            spikes[numerics[i]].OnMove(activateX);
        }
    }
    public void DeactivateAll()
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].OnMove(deactivateX);
        }
    }
    private int[] RandomNumerics(int maxCount, int n)
    {
        // 0 ~ maxCount까지의 숫자 중 겹치지 않는 n개의 난수가 필요할 때 사용
        int[] defaults = new int[maxCount];     // 0 ~ maxCount까지 순서대로 저장하는 배열
        int[] results = new int[n];             // 결과 값들을 저장하는 배열

        // 배열 전체에 0부터 maxCount의 값을 순서대로 저장
        for(int i = 0; i < maxCount; i++)
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
}
