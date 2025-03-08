using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score;

    private void Awake()
    {
        score = GameManager.Instance.GetScore();
        UpdateScoreText();
    }

    private void Update()
    {
        score = GameManager.Instance.GetScore(); 
        if(SceneManager.GetActiveScene().name == "GameLevel")
        {
            UpdateInGameScoreText();
        }
    }

    private void UpdateInGameScoreText()
    {
        if (score < 10)
        {
            scoreText.text = "0" + score.ToString();
        }
        else
        {
            scoreText.text = score.ToString();
        }
    }

    public void UpdateScoreText()
    {
        if (SceneManager.GetActiveScene().name == "DeathScreen")
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
