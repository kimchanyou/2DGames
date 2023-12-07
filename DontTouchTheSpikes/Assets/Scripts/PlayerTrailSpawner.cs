using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrailSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private GameObject[] trailEffects;
    [SerializeField]
    private int maxCount = 4;
    [SerializeField]
    private float duration = 0.1f;

    public void OnSpawns()
    {
        StartCoroutine("SpawnProcess");
    }

    private IEnumerator SpawnProcess()
    {
        int currentIndex = 0;
        float beginTime = Time.time;

        while(currentIndex < maxCount)
        {
            float t = (Time.time - beginTime) / duration;

            if (t >= 1)
            {
                for(int i = 0; i < trailEffects.Length; i++)
                {
                    if (!trailEffects[i].activeSelf)
                    {
                        trailEffects[i].SetActive(true);
                        trailEffects[i].GetComponent<PlayerTrailEffect>().spriteRenderer.flipX = playerTransform.GetComponent<SpriteRenderer>().flipX;
                        trailEffects[i].transform.position = playerTransform.position;
                        break;
                    }
                }

                currentIndex++;
                beginTime = Time.time;
            }

            yield return null;
        }

    }
}
