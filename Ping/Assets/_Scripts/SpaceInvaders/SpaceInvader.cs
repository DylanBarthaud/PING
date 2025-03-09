using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvader : MonoBehaviour, IHitable
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Animator animator;

    private bool isDying = false; 

    public void OnHit()
    {
        speed = 0;

        if (isDying)
        {
            return; 
        }

        isDying = true;

        audioSource.Play();
        animator.Play("SpaceInvaderDeath"); 
        EventsSystem.Current.OnAlienDestroyed();

        Destroy(gameObject, audioSource.clip.length);
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0,-1,0) * speed * Time.deltaTime;
    }
}
