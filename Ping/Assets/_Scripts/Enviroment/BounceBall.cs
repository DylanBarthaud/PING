using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Vector2 collisionNormal = collision.GetContact(0).normal;

            Direction bounceDirection;
            if (Mathf.Abs(collisionNormal.x) > Mathf.Abs(collisionNormal.y))
            {
                bounceDirection = Direction.X;
            }
            else
            {
                bounceDirection = Direction.Y;
            }

            collision.gameObject.GetComponent<Ball>().Bounce(bounceDirection);
        }
    }
}
