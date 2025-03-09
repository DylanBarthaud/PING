using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestroyObjectOnCollide : MonoBehaviour
{
    [SerializeField]
    private string[] objTags;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(string tag in objTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                collision.gameObject.GetComponent<IHitable>().OnHit();
            }
        }
    }
}
