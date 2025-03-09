using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSystem : MonoBehaviour
{
    private static EventsSystem current;
    public static EventsSystem Current
    {
        get
        {
            if (current == null)
            {
                Debug.LogWarning("EventSystem is null");
            }
            return current;
        }
    }

    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    public event Action onBallDestroyed; 
    public event Action onAlienDestroyed;
    public event Action onSnakeDestroyed;
    public event Action onAstroidDestroyed; 
    public void OnBallDestroyed()
    {
        onBallDestroyed?.Invoke();
    }

    public void OnAlienDestroyed()
    {
        onAlienDestroyed?.Invoke();
    }

    public void OnSnakeDestroyed()
    {
        onSnakeDestroyed?.Invoke();
    }

    public void OnAstroidDestroyed()
    {
        onAstroidDestroyed?.Invoke();
    }
}
