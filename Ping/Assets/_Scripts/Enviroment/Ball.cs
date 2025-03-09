using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Ball : MonoBehaviour, IHitable
{
    #region Vars
    [SerializeField]
    private Color baseColor, onCDColor; 

    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float baseChangeDirctCD;
    private float speed;
    private float changeDirectCD;

    private AudioSource audioSource;
    private Collider2D col;

    Vector2 direction;
    #endregion

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        col.enabled = false; 
        Invoke(nameof(EnableCollision), 0.5f); 
    }

    private void Start()
    {
        direction = new Vector2(Utils.Flip(), Utils.Flip());

        speed = baseSpeed;
        changeDirectCD = -1; 
    }

    private void Update()
    {
        speed += Time.deltaTime / 10;
        baseSpeed += Time.deltaTime / 100;

        #region Controls
        if (changeDirectCD <= 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = baseColor; 

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (direction.y == 1)
                {
                    return;
                }

                //Changes balls direction to "upwards",
                //adds speed boost
                direction.y = 1;

                Dash(4);

                changeDirectCD = baseChangeDirctCD;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (direction.y == -1)
                {
                    return;
                }

                //Changes balls direction to "downwards",
                //adds speed boost
                direction.y = -1;

                Dash(4);

                changeDirectCD = baseChangeDirctCD;
            }
        }

        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = onCDColor;
            changeDirectCD -= Time.deltaTime;
        }

        #endregion
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
    }

    private void EnableCollision()
    {
        col.enabled = true;
    }

    private void Dash(float dashPW)
    {
        speed *= dashPW;
        Invoke("StopDash", 0.1f); 
    }

    private void StopDash()
    {
        speed = baseSpeed; 
    }

    /// <summary>
    /// Causes the ball to change it's direction
    /// </summary>
    /// <param name="changeDirection"> Direction to bounce </param>
    public void Bounce(Direction changeDirection)
    {
        audioSource.Play(); 

        if (changeDirection == Direction.X)
        {
            direction.x = -direction.x;
        }
        else
        {
            direction.y = -direction.y;
        }

        speed = baseSpeed;
    }

    public void DestroyBall()
    {
        EventsSystem.Current.OnBallDestroyed(); 
        Destroy(gameObject);
    }

    public void OnHit()
    {
        DestroyBall();
    }
}
