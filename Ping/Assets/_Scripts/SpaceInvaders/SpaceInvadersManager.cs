using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spaceInvader;

    [SerializeField]
    private float baseSpawnCD;
    private float spawnCD; 

    void Update()
    {
        if(spawnCD <= 0)
        {
            Instantiate(
                spaceInvader, 
                new Vector3(
                    Random.Range(-8,8), 
                    transform.position.y,
                    transform.position.z
                    ), 
                Quaternion.identity
                );
            spawnCD = baseSpawnCD; 
        }

        else
        {
            spawnCD -= Time.deltaTime;  
        }
    }
}
