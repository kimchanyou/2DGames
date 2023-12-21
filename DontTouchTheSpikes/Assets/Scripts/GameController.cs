using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using UnityEngine;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour
{
    public LightSpawner[] lightSpawners;


    public SpikeSpawner[] spikeSpawners;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Heart heart;

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
        GoogleManager.Instance.RequestBanner();
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
        int rand = Random.Range(0, 100);
        if (rand < 95)
            return;
        if (!heart.gameObject.activeSelf)
        {
            heart.gameObject.SetActive(true);
            if (currentScore % 2 == 0)
                heart.transform.position = new Vector2(2.5f, Random.Range(-5.5f, 5.5f));
            else
                heart.transform.position = new Vector2(-2.5f, Random.Range(-5.5f, 5.5f));
            heart.originY = heart.transform.position.y;
        }
    }

    public void GameOver()
    {
        if (currentScore > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", currentScore);
            GoogleManager.Instance.AddLeaderboard(currentScore);
        }
        uiController.GameOver();
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
    public void GameStart()
    {
        player.GameStart();
        uiController.GameStart();
        foreach (LightSpawner spawner in lightSpawners)
        {
            spawner.GetComponent<BackGroundLoop>().GameStart();
        }
    }

    public void GameReStart()
    {
        GoogleManager.Instance.RequestInterstitial();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    //private IEnumerator GameStart()
    //{
    //    while (true)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            player.GameStart();
    //            uiController.GameStart();
    //            foreach (LightSpawner spawner in lightSpawners)
    //            {
    //                spawner.GetComponent<BackGroundLoop>().GameStart();
    //                //spawner.GetComponent<BackGroundLoop>().isGameStart = true;

    //            }

    //            yield break;
    //        }
    //        yield return null;
    //    }
    //}
    //private IEnumerator GameOverProcess()
    //{
    //    if (currentScore > PlayerPrefs.GetInt("HIGHSCORE"))
    //    {
    //        PlayerPrefs.SetInt("HIGHSCORE", currentScore);
    //    }

    //    uiController.GameOver();

    //    while (true)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    //        }
    //        yield return null;
    //    }
    //}
}
