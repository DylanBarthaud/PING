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
    [SerializeField]
    private GameObject spaceShipPreset;

    [SerializeField]
    private GameObject spaceInvadersManager;

    [SerializeField]
    private GameObject Snake; 

    //Stat data
    private int level;

    private int maxBalls;
    private int ballsLeft;

    private int overallScore;
    private int currentScore;

    private int aliensKilled; 
    #endregion

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        EventsSystem.Current.onBallDestroyed += OnBallDestroyed;
        EventsSystem.Current.onAlienDestroyed += OnAlienDestroyed; 
    }

    private void OnAlienDestroyed()
    {
        aliensKilled += 1; 
    }

    private void OnBallDestroyed()
    {
        ballsLeft -= 1; 
        if(ballsLeft <= 0)
        {
            LoadDeathScreen();
        }
    }

    public void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreen");
        overallScore += currentScore;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameLevel")
        {
            SnakeLoaded = false;
            SpaceInvadersLoaded = false;
            currentScore = 0; 
            LoadBallObjects();
        }
    }

    /// <summary>
    /// Loads needed ball into the game level
    /// </summary>
    public void LoadBallObjects()
    {
        if(ballsLeft > 40)
        {
            Debug.LogWarning("Too many bullets");
            return;
        }

        Instantiate(ballPreset, new Vector3(0, 0, 0), transform.rotation);
        ballsLeft += 1; 
    }

    private bool SpaceInvadersLoaded = false;
    private void LoadSpaceInvaders()
    {
        Instantiate(spaceInvadersManager, new Vector3(0, 9f, 0), Quaternion.identity);

        Instantiate(spaceShipPreset, new Vector3(0, -4.74f, 0), Quaternion.identity);
        SpaceInvadersLoaded = true;
    }

    private bool SnakeLoaded = false;
    private void LoadSnake()
    {
        Instantiate(Snake, new Vector3(-8,-4,0), Quaternion.identity);  
        SnakeLoaded = true;
    }

    public void AddPoint()
    {
        currentScore += 1; 

        if(currentScore >= 15 && !SpaceInvadersLoaded)
        {
            LoadSpaceInvaders();
        }

        else if(currentScore >= 20 && !SnakeLoaded)
        {
            LoadSnake();
        }
    }

    public int GetScore()
    {
        return currentScore; 
    }
}
