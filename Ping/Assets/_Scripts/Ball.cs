using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Vars
    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float baseChangeDirctCD;
    [SerializeField]
    private float speed;
    private float changeSpeedCD;

    private bool isDashing = false; 

    Vector2 direction;
    #endregion

    private void Start()
    {
        direction = new Vector2(Utils.Flip(), Utils.Flip());

        speed = baseSpeed;
        changeSpeedCD = -1; 
    }

    private void Update()
    {
        speed += Time.deltaTime / 10;
        baseSpeed += Time.deltaTime / 100;

        #region Controls
        if (changeSpeedCD <= 0)
        {
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

                changeSpeedCD = baseChangeDirctCD;
            }
        }

        else
        {
            changeSpeedCD -= Time.deltaTime;
        }

        #endregion
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
    }

    private void Dash(float dashPW)
    {
        speed *= dashPW;
        isDashing = true; 
        Invoke("StopDash", 0.1f); 
    }

    private void StopDash()
    {
        speed = baseSpeed; 
        isDashing = false;
    }

    /// <summary>
    /// Causes the ball to change it's direction
    /// </summary>
    /// <param name="changeDirection"> Direction to bounce </param>
    public void Bounce(Direction changeDirection)
    {
        if(changeDirection == Direction.X)
        {
            direction.x = -direction.x;
        }
        else
        {
            direction.y = -direction.y;
        }

        Dash(2f);
    }

    public void DestroyBall()
    {
        EventsSystem.Current.OnBallDestroyed(); 
        Destroy(gameObject);
    }
}
