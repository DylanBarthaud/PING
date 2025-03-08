using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText; 

    private void Update()
    {
        int score = GameManager.Instance.GetScore(); 
        if(score < 10)
        {
            scoreText.text = "0" + score.ToString();
        }
        else
        {
            scoreText.text = score.ToString();
        }
       
    }
}
