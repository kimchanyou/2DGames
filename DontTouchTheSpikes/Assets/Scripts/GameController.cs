using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private LightSpawner[] lightSpawners;


    public SpikeSpawner[] spikeSpawners;
    [SerializeField]
    private Player player;
    private UIController uiController;
    private RandomColor randomColor;
    private int currentSpawn = 0;
    public int currentScore = 0;
    private int colorChange = 1;
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
        // 반대 방향의 가시 활성화
        UpdateSpikes();

        // 점수 증가, 점수 UI 업데이트
        currentScore++;
        uiController.UpdateScore(currentScore);

        if (colorChange % 5 == 0)
            randomColor.OnChangeColor();
        // 배경 색상, 현재 점수 Text UI 색상 변경
        //if (colorChange % randomColor.randomNum == 0)
        //{
        //    randomColor.OnChange();
        //    colorChange = 0;
        //}
        colorChange++;
    }

    public void GameOver()
    {
        StartCoroutine("GameOverProcess");
    }

    private void UpdateSpikes()
    {
        // 왼쪽 가시가 활성화되면 오른쪽 가시는 비활성화
        // 오른쪽 가시가 활성화되면 왼쪽 가시는 비활성화

        //spikeSpawners[currentSpawn].ActivateAll();

        for (int i = 0; i < lightSpawners.Length; i++)
        {
            if (currentSpawn % 2 == 0)
            {
                if (i < 3)
                    lightSpawners[i].ActivateAll();
                else
                    lightSpawners[i].DeactivateAll();
            }
            else
            {
                if (i < 3)
                    lightSpawners[i].DeactivateAll();
                else
                    lightSpawners[i].ActivateAll();
            }
        }
        currentSpawn = (currentSpawn + 1) % 2;
        //spikeSpawners[currentSpawn].DeactivateAll();
    }
    private IEnumerator GameStart()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.GameStart();
                uiController.GameStart();
                foreach (LightSpawner spawner in lightSpawners)
                {
                    spawner.GetComponent<BackGroundLoop>().GameStart();
                    //spawner.GetComponent<BackGroundLoop>().isGameStart = true;

                }

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
