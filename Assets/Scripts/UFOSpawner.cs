using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    public GameObject UFOPrefab;
    public float minSpawnInterval = 7f;
    public float maxSpawnInterval = 10f;
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
            SpawnUFO();
            maxTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    void SpawnUFO()
    {
        Instantiate(UFOPrefab, transform.position, Quaternion.identity);
    }

    public void SetSpawnState(bool state)
    {
        canSpawn = state;
    }
}
