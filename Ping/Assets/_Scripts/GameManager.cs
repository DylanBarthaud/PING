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

    [SerializeField]
    private GameObject astroidSpawner; 

    //Stat data
    private int level;

    private int maxBalls;
    private int ballsLeft;

    private int overallScore;
    private int currentScore;

    private int threatMeter; 

    private int aliensKilled;

    private bool shipLoaded; 
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
            currentScore = 0; 
            LoadBallObject();
        }
    }

    /// <summary>
    /// Loads needed ball into the game level
    /// </summary>
    public void LoadBallObject()
    {
        if(ballsLeft > 40)
        {
            Debug.LogWarning("Too many bullets");
            return;
        }

        Instantiate(ballPreset, new Vector3(0, 0, 0), transform.rotation);
        ballsLeft += 1; 
    }

    private void LoadSpaceInvaders()
    {
        Instantiate(spaceInvadersManager, new Vector3(0, 9f, 0), Quaternion.identity);
    }

    private void LoadSnake()
    {
        Instantiate(Snake, new Vector3(-8,-4,0), Quaternion.identity);  
    }

    private void LoadAstroids()
    {
        Instantiate(astroidSpawner, new Vector3(-20, 15, 0), Quaternion.identity);
        Instantiate(astroidSpawner, new Vector3(20, -15, 0), Quaternion.identity);
        Instantiate(astroidSpawner, new Vector3(20, 15, 0), Quaternion.identity);
        Instantiate(astroidSpawner, new Vector3(-20, -15, 0), Quaternion.identity);
    }

    public void AddPoint()
    {
        currentScore += 1; 
        LoadBallObject();

        threatMeter += 1;  
        if(currentScore >= 10 && !shipLoaded)
        {
            Instantiate(spaceShipPreset, new Vector3(0, -4.74f, 0), Quaternion.identity);
            shipLoaded = true; 
        }

        if(threatMeter >= 10)
        {
            spawnThreat();
            threatMeter = 0; 
        }
    }

    private void spawnThreat()
    {
        int r = Random.Range(0, 3);

        switch (r)
        {
            case 0:
                LoadSpaceInvaders();
                break;
            case 1:
                LoadSnake();
                break;
            case 2:
                LoadAstroids();
                break; 
        }
    }

    public int GetScore()
    {
        return currentScore; 
    }
}
