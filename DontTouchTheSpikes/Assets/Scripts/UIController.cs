using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private TextMeshProUGUI highScore;

    [Header("InGame")]
    [SerializeField]
    private GameObject inGamePanel;
    [SerializeField]
    private TextMeshProUGUI textScore;

    [Header("GameOver")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI textHighScore;

    private void Start()
    {
        highScore.text = $"HIGH SCORE : {PlayerPrefs.GetInt("HIGHSCORE")}";
    }
    public void GameStart()
    {
        mainPanel.SetActive(false);
        inGamePanel.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        if (score < 10)
        {
            textScore.text = score.ToString("D2");
        }
        else
        {
            textScore.text = score.ToString();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        textHighScore.text = $"HIGH SCORE : {PlayerPrefs.GetInt("HIGHSCORE")}";
    }
}
