using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    #region Vars
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
        flipCD = 0; 
    }

    private void Update()
    {
        if(flipCD <= 0)
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
