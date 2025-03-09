using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour, IHitable
{
    [SerializeField]
    private GameObject[] Astroids; 

    [SerializeField]
    private float speed; 
    private Vector2 direction;
    private Collider2D col;

    private int size = 3; 
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
        Invoke(nameof(EnableCollision), 0.2f);
    }

    private void Start()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (xPos < 0 && yPos < 0)
        {
            direction = new Vector2(Random.Range(1,2), Random.Range(1, 2));
        }
        else if (xPos < 0 && yPos > 0)
        {
            direction = new Vector2(Random.Range(1, 2), Random.Range(-1, -2));
        }
        else if (xPos > 0 && yPos < 0)
        {
            direction = new Vector2(Random.Range(-1, -2), Random.Range(1, 2)); 
        }
        else if (xPos > 0 && yPos > 0)
        {
            direction = new Vector2(Random.Range(-1, -2), Random.Range(-1, -2));
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime; 
    }

    private void EnableCollision()
    {
        col.enabled = true; 
    }

    public void SetSize(int size)
    {
        this.size = size;
        transform.localScale = 0.3f * size * Vector3.one;
    }

    public void OnHit()
    {
        if (size - 1 != 0)
        {
            Astroid newAstroid;
            newAstroid = Instantiate(
                Astroids[Random.Range(0, Astroids.Length)],
                transform.position,
                Quaternion.identity
                ).GetComponent<Astroid>();

            newAstroid.SetSize(size - 1);
        }

        EventsSystem.Current.OnAstroidDestroyed(); 
        Destroy(gameObject);
    }
}
