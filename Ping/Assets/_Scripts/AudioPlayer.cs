using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource astroidAudio, snakeAudio;

    private void Start()
    {
        EventsSystem.Current.onAstroidDestroyed += OnAstroidDestroyed;
        EventsSystem.Current.onSnakeDestroyed += OnSnakeDestroyed; 
    }

    private void OnAstroidDestroyed()
    {
        if (astroidAudio == null)
        {
            return;
        }

        astroidAudio.Play(); 
    }

    private void OnSnakeDestroyed()
    {
        if(snakeAudio == null)
        {
            return;
        }

        snakeAudio.Play();
    }
}
