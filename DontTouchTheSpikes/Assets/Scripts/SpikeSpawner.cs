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
        // 0 ~ maxCount������ ���� �� ��ġ�� �ʴ� n���� ������ �ʿ��� �� ���
        int[] defaults = new int[maxCount];     // 0 ~ maxCount���� ������� �����ϴ� �迭
        int[] results = new int[n];             // ��� ������ �����ϴ� �迭

        // �迭 ��ü�� 0���� maxCount�� ���� ������� ����
        for(int i = 0; i < maxCount; i++)
        {
            defaults[i] = i;
        }

        for (int i = 0; i < n; i++)
        {
            int index = Random.Range(0, maxCount);  // ������ ���ڸ� �ϳ� �̾Ƽ�

            results[i] = defaults[index];
            defaults[index] = defaults[maxCount - 1];

            maxCount--;
        }

        return results;
    }
}
