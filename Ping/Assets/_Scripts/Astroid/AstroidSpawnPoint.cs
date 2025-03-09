using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private float baseSpawnCD;
    [SerializeField]
    private float spawnCD;

    [SerializeField]
    private GameObject astroidPrefab;

    private void Start()
    {
        Invoke(nameof(Die), 100); 
    }

    void Update()
    {
        if(spawnCD < 0)
        {
            Astroid astroid = Instantiate(astroidPrefab, new Vector3(
                    transform.position.x + Random.Range(1,7),
                    transform.position.y + Random.Range(1,7),
                    transform.position.z),
                Quaternion.identity).GetComponent<Astroid>();

            astroid.SetSize(3);

            spawnCD = Random.Range(baseSpawnCD, baseSpawnCD*2); 
        }
        else
        {
            spawnCD -= Time.deltaTime;
        }
    }

    private void Die()
    {

    }
}
