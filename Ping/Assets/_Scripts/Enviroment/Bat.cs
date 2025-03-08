using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    #region Vars
    [SerializeField]
    private GameObject BulletPrefab; 
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private float baseShootCD;
    [SerializeField]
    private float shootCD;

    [SerializeField]
    private int baseSpeed;
    [SerializeField]
    private float baseflipCD;
    private float flipCD; 
    private int speed;

    private int direction; 
    #endregion

    private void Start()
    {
        direction = Utils.Flip();
        speed = baseSpeed; 
        shootCD = baseShootCD;
        flipCD = 0; 
    }

    private void Update()
    {
        if (transform.localScale.y < 2)
        {
            Vector3 newScale = transform.localScale;
            newScale.y += Time.deltaTime / 300;
            transform.localScale = newScale;
        }

        if (flipCD <= 0)
        {
            direction = Utils.Flip();
            flipCD = Random.Range(baseflipCD/2, baseflipCD); 
        }

        else
        {
            flipCD -= Time.deltaTime;
        }

        if(transform.position.y >= 3.5 
            || transform.position.y <= -3.5)
        {
            transform.position = transform.position.y >= 3.5f 
                ? new Vector3(transform.position.x, 3.5f, transform.position.z)
                : new Vector3(transform.position.x, -3.5f, transform.position.z);

            direction = -direction;
        }

        if(GameManager.Instance.GetScore() < 5)
        {
            return; 
        }

        if (baseShootCD > 1)
        {
            baseShootCD -= Time.deltaTime / 100;
        }

        if (shootCD <= 0)
        {
            Bullet bullet = Instantiate(BulletPrefab, shootPoint.transform.position, Quaternion.identity).GetComponent<Bullet>();

            Vector2 bulletDirect; 
            if(transform.position.x > 0)
            {
                bulletDirect = new Vector2(-1, 0);
            }
            else { bulletDirect = new Vector2(1, 0); }

            bullet.SetDirection(bulletDirect);
            shootCD = Random.Range(baseShootCD/2, baseShootCD);
        }
        else
        {
            shootCD -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, direction, 0) * speed * Time.deltaTime; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Ball>().Bounce(Direction.X);
            GameManager.Instance.AddPoint(); 
        }
    }
}
