using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float speed; 
    private int direction = 0;

    [SerializeField]
    private AudioSource audioSource; 

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shootPoint; 
    [SerializeField]
    private float baseShootCD;
    private float shootCD;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }

        if(shootCD <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                shootCD = baseShootCD; 
            }
        }
        else
        {
            shootCD -= Time.deltaTime;
        }

        if(transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;
    }

    private void Shoot()
    {
        audioSource.Play(); 
        Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity).GetComponent<Bullet>();

        bullet.SetDirection(new Vector2(0, 1));
    }
}
