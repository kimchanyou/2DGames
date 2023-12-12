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
    private int colorChange = 0;
    private void Awake()
    {
        uiController = GetComponent<UIController>();
        randomColor = GetComponent<RandomColor>();
    }

    private void Start()
    {
        StartCoroutine("GameStart");
    }

    public void CollisionWithWall()
    {
        // �ݴ� ������ ���� Ȱ��ȭ
        UpdateSpikes();

        // ���� ����, ���� UI ������Ʈ
        currentScore++;
        uiController.UpdateScore(currentScore);

        // ��� ����, ���� ���� Text UI ���� ����
        if (colorChange % randomColor.randomNum == 0)
        {
            randomColor.OnChange();
            colorChange = 0;
        }
        colorChange++;
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
    private IEnumerator GameStart()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.GameStart();
                uiController.GameStart();

                yield break;
            }
            yield return null;
        }
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
            }
            yield return null;
        }
    }
}
