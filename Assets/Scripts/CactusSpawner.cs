using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSpawner : MonoBehaviour
{
    public GameObject[] cactusPrefabs;  
    public Transform spawnPoint;        
    public float minSpawnInterval = 2f; 
    public float maxSpawnInterval = 5f;
    private float timer = 0;
    private float maxTime;
    public bool canSpawn = true;

    private void Start()
    {
        maxTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void Update()
    {
        if (!canSpawn)
            return;

        if (timer > maxTime)
        {
            SpawnCactus();
            maxTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    void SpawnCactus()
    {

        int randomIndex = Random.Range(0, cactusPrefabs.Length - 1);
        GameObject cactusToSpawn = cactusPrefabs[randomIndex];

        Instantiate(cactusToSpawn, spawnPoint.position, Quaternion.identity);
    }

    public void SetSpawnState(bool state)
    {
        canSpawn = state;
    }
}
