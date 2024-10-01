using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public Transform spawnHighPoint;
    public Transform spawnLowPoint;
    public float minSpawnInterval = 4f;
    public float maxSpawnInterval = 8f;
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
            SpawnBird();
            maxTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    void SpawnBird()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(spawnHighPoint.position.y, spawnLowPoint.position.y), transform.position.z);
        Instantiate(birdPrefab, spawnPoint, Quaternion.identity);
    }

    public void SetSpawnState(bool state)
    {
        canSpawn = state;
    }
}
