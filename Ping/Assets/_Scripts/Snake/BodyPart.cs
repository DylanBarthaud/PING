using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour, IHitable
{
    [SerializeField]
    private int health; 

    private Snake head;
    private int indexInSnake; 

    public void OnHit()
    {
        health -= 1; 
        if(health <= 0)
        {
            head.DestroySegments(indexInSnake);
        }
    }

    public void SetHead(Snake head)
    {
        this.head = head;
    }

    public void setIndex(int index)
    {
        indexInSnake = index;
    }
}
