using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    private void Start()
    {
        InvokeRepeating("InstantiateEnemy", 0f, Random.Range(3f, 10f));
    }
    
    void InstantiateEnemy()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        GameObject randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        Instantiate(prefabToInstantiate, randomSpawnPoint.transform.position, Quaternion.identity);
    }
}

