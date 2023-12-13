using System.Collections;
using UnityEngine;


public class SpikeSpawner : MonoBehaviour
{
    [SerializeField]
    private Spike[] spikes;
    [SerializeField]
    private Wall wall;
    [SerializeField]
    private float activateX;
    [SerializeField]
    private float deactivateX;

    public float moveTime = 2.0f;
    private Vector2[] spikesPos;
    private Vector2 startPos;
    private void Start()
    {
        startPos = transform.position;
        spikesPos = new Vector2[spikes.Length];
        for(int i = 0; i < spikes.Length; i++)
        {
            spikesPos[i] = new Vector2(spikes[i].transform.position.x, spikes[i].transform.position.y);
        }
    }
    public void ActivateAll()
    {
        int count = Random.Range(3, spikes.Length - 3);

        int[] numerics = RandomNumerics(spikes.Length, count);

        wall.OnMove(wall.activateX);
        for(int i = 0; i < numerics.Length; i++)
        {
            spikes[numerics[i]].OnMove(activateX, spikes[numerics[i]].transform.position.y - 1.2f);
        }
        OnMove(-3);
    }
    public void DeactivateAll()
    {
        wall.OnMove(wall.deactivateX);
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].OnMove(deactivateX, spikes[i].transform.position.y - 1.2f);
        }
        StartCoroutine(CoPosReset());
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

    public void OnMove(float y)
    {
        Vector2 start = transform.position;
        Vector2 end = new Vector2(transform.position.x, y);

        StartCoroutine(MoveProcess(start, end));
    }

    private IEnumerator CoPosReset()
    {
        yield return new WaitForSeconds(0.55f);

        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].transform.position = spikesPos[i];
        }
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
        transform.position = startPos;
    }
}
