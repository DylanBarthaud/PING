using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public enum Direction { X, Y }; 

public class GameManager : MonoBehaviour
{
    #region Vars
    private static GameManager instance;
    public static GameManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                Debug.LogWarning("GameManager is null");
            }
            return instance; 
        }
    }

    [SerializeField]
    private GameObject ballPreset;

    //Stat data
    private int level;

    private int maxBalls;
    private int ballsLeft;

    private int overallScore;
    private int currentScore;
    #endregion

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        EventsSystem.Current.onBallDestroyed += LoadMenu; 
    }

    public void LoadGameLevel(){; 
        SceneManager.LoadScene("GameLevel"); 
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameLevel")
        {
            LoadGameObjects();
        }
    }

    /// <summary>
    /// Loads ball and bats into the game level
    /// </summary>
    private void LoadGameObjects()
    {
        Instantiate(ballPreset, new Vector3(0, 0, 0), transform.rotation);
    }

    public void AddPoint()
    {
        currentScore += 1; 
    }

    public int GetScore()
    {
        return currentScore; 
    }
}
