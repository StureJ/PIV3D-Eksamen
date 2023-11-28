using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //The prefab it has to instantiate
    public GameObject prefabToInstantiate;

    private void Start()
    {
        //It runs the function InstantiateEnemy once every 1 to 5 seconds (randomly)
        InvokeRepeating("InstantiateEnemy", 0f, Random.Range(1f, 5f));
    }
    
    void InstantiateEnemy()
    {
        //It finds all the gameobjects with the tag SpawnPoint and puts them in spawnPoints
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        //It creates a variable named randomSpawnPoint and chooses a random spawnpoint
        GameObject randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        //It instantiates the prefab at the random spawnpoint
        Instantiate(prefabToInstantiate, randomSpawnPoint.transform.position, Quaternion.identity);
    }
}

