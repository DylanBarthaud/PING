using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IHitable
{
    #region Vars
    private const int BOUNDS_X_MIN = -8;
    private const int BOUNDS_Y_MIN = -4;
    private const int BOUNDS_X_MAX = 8;
    private const int BOUNDS_Y_MAX = 4;

    [SerializeField]
    private GameObject bodyObj;
    List<GameObject> bodyParts = new List<GameObject>();

    [SerializeField]
    private int health; 

    [SerializeField]
    private int speed;
    private float stepCD;

    private Vector2 direction = new Vector2(0,0);
    [SerializeField]
    private float baseDirectChangeCD;
    private float directChangeCD;

    [SerializeField]
    private AudioSource audioSource;
    #endregion

    private void Start()
    {
        bodyParts.Add(gameObject);
    }

    private void Update()
    {
        if(stepCD <= 0)
        {
            Step();
            stepCD = speed; 
        }
        else
        {
            stepCD -= Time.deltaTime;
        }

        if(directChangeCD <= 0)
        {
            ChangeDirection();
            directChangeCD = baseDirectChangeCD; 
        }
        else
        {
            directChangeCD -= Time.deltaTime;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Threat")
            || collision.gameObject.CompareTag("Ball"))
        {
            Grow();
        }
    }

    private void Step()
    {
        // Check if the next position would hit a wall
        Vector3 nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
        if (nextPosition.x < BOUNDS_X_MIN 
            || nextPosition.x > BOUNDS_X_MAX 
            || nextPosition.y < BOUNDS_Y_MIN 
            || nextPosition.y > BOUNDS_Y_MAX)
        {
            ChangeDirection();
        }

        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].transform.position = bodyParts[i - 1].transform.position;
        }

        transform.position += new Vector3(
            direction.x,
            direction.y,
            0); 
    }

    private void ChangeDirection()
    {
        List<Vector2> directions = new List<Vector2>()
        {
            new Vector2(0,1),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(-1,0),
        };

        directions.RemoveAll(d => d == -direction);

        directions.RemoveAll(d =>
            (transform.position.x + d.x < BOUNDS_X_MIN || transform.position.x + d.x > BOUNDS_X_MAX) ||
            (transform.position.y + d.y < BOUNDS_Y_MIN || transform.position.y + d.y > BOUNDS_Y_MAX));

        directions.RemoveAll(d => (Vector2)transform.position + d == new Vector2(0,0));

        direction = directions[Random.Range(0,directions.Count)];

        //Flip snake head to face correct direction
        if (direction.Equals(new Vector2(0, 1)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.Equals(new Vector2(0, -1)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction.Equals(new Vector2(1, 0)))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (direction.Equals(new Vector2(-1, 0)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

    }

    private void Grow()
    {
        print("hgere");
        GameObject newSegment = Instantiate(bodyObj); 

        newSegment.transform.position = bodyParts[bodyParts.Count - 1].transform.position;
        BodyPart segmentScr = newSegment.gameObject.GetComponent<BodyPart>();
        segmentScr.SetHead(this);
        segmentScr.setIndex(bodyParts.Count);

        bodyParts.Add(newSegment);
    }

    /// <summary>
    /// Destroys segments of snake.
    /// </summary>
    /// <param name="startIndex"> the last segment to be destroyed </param>
    public void DestroySegments(int startIndex)
    {
        PlaySound();

        for (int i = startIndex; i < bodyParts.Count; i++)
        {
            Destroy(bodyParts[i]);
        }
        bodyParts.RemoveRange(startIndex, bodyParts.Count - startIndex);
    }

    public void OnHit()
    {
        health -= 1; 
        if(health <= 0)
        {
            EventsSystem.Current.OnSnakeDestroyed();
            DestroySegments(0); 
            Destroy(gameObject);
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
