using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public CactusSpawner cactusSpawner;
    public BirdSpawner birdSpawner;
    public UFOSpawner UFOSpawner;
    public Egg egg1;
    public Egg egg2;
    public Egg egg3;
    private float score;

    public Transform BossSpawnPoint;
    public GameObject BossPrefab;

    bool egg1Spawned = false;
    bool egg2Spawned = false;
    bool egg3Spawned = false;

    private float nextBossSpawnScore = 1000f;

    private void Awake()
    {
        cactusSpawner.SetSpawnState(true);
        birdSpawner.SetSpawnState(false);
        UFOSpawner.SetSpawnState(false);
    }
    private void Update()
    {
        score = GameManager.instance.GetScore();
        if (score >= 100 && score < 150)
        {
            cactusSpawner.SetSpawnState(false);
            if (score >= 125 && !egg1Spawned)
            {
                EggSpawner.instance.SpawnEgg(egg1);
                egg1Spawned = true;
            }
        }
        else if (score >= 150 && score < 250)
        {
            cactusSpawner.SetSpawnState(true);
            birdSpawner.SetSpawnState(true);
        }
        else if (score >= 250 && score < 300 )
        {
            cactusSpawner.SetSpawnState(false);
            birdSpawner.SetSpawnState(false);
            if (score >= 275 && !egg2Spawned)
            {
                EggSpawner.instance.SpawnEgg(egg2);
                egg2Spawned = true;
            }

        }
        else if (score >= 300 && score < 400)
        {
            cactusSpawner.SetSpawnState(true);
            birdSpawner.SetSpawnState(true);
            UFOSpawner.SetSpawnState(true);
        }
        else if (score >= 400 && score < 450 )
        {
            cactusSpawner.SetSpawnState(false);
            birdSpawner.SetSpawnState(false);
            UFOSpawner.SetSpawnState(false);
            if (score >= 425 && !egg3Spawned)
            {
                EggSpawner.instance.SpawnEgg(egg3);
                egg3Spawned = true;
            }
        }
        else if (score >= 450)
        {
            cactusSpawner.SetSpawnState(true);
            birdSpawner.SetSpawnState(true);
            UFOSpawner.SetSpawnState(true);
        }

        if (score >= nextBossSpawnScore)
        {
            Instantiate(BossPrefab, BossSpawnPoint.position, Quaternion.identity);
            nextBossSpawnScore += 1500f;
        }


    }
}
