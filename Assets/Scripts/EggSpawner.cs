using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public static EggSpawner instance;

    private void Awake()
    {
        instance = this;

    }
    public void SpawnEgg(Egg eggPrefab)
    {
        
        Instantiate(eggPrefab, transform.position, Quaternion.identity);
    }
}
