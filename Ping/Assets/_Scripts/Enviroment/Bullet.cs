using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHitable
{
    private Vector2 direction;
    [SerializeField]
    private float speed; 

    private void FixedUpdate()
    {
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 setDirect)
    {
        direction = setDirect;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void OnHit()
    {
        DestroyBullet();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); 
    }
}
