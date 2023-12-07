using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SpikeSpawner[] spikeSpawners;
    [SerializeField]
    private Player player;
    private UIController uiController;
    private RandomColor randomColor;
    private int currentSpawn = 0;
    private int currentScore = 0;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        randomColor = GetComponent<RandomColor>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                player.GameStart();
                uiController.GameStart();

                yield break;
            }
            yield return null;
        }
    }

    public void CollisionWithWall()
    {
        // �ݴ� ������ ���� Ȱ��ȭ
        UpdateSpikes();

        // ���� ����, ���� UI ������Ʈ
        currentScore++;
        uiController.UpdateScore(currentScore);

        // ��� ����, ���� ���� Text UI ���� ����
        randomColor.OnChange();
    }

    public void GameOver()
    {
        StartCoroutine("GameOverProcess");
    }

    private void UpdateSpikes()
    {
        // ���� ���ð� Ȱ��ȭ�Ǹ� ������ ���ô� ��Ȱ��ȭ
        // ������ ���ð� Ȱ��ȭ�Ǹ� ���� ���ô� ��Ȱ��ȭ

        spikeSpawners[currentSpawn].ActivateAll();

        currentSpawn = (currentSpawn + 1) % spikeSpawners.Length;

        spikeSpawners[currentSpawn].DeactivateAll();
    }
    private IEnumerator GameOverProcess()
    {
        if (currentScore > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", currentScore);
        }

        uiController.GameOver();

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            yield return null;
        }
    }
}
