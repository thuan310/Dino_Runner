using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreathAnimation : MonoBehaviour
{
    [SerializeField] private Transform fireBreathSpawnPoint;
    [SerializeField] private GameObject fireBreadthPrefab;
    [SerializeField] private Transform SlamSpawnPoint;
    [SerializeField] private GameObject SlamPrefab;
    private GameObject fireBreath;
    private GameObject slam;

    public void CreateFireBreath()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Breath);
        fireBreath = Instantiate(fireBreadthPrefab, fireBreathSpawnPoint.position, Quaternion.identity);
        fireBreath.SetActive(true);
    }

    public void DeleteFireBreath()
    {
        Destroy( fireBreath );
    }

    public void CreateSlam()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Explosion);
        slam = Instantiate(SlamPrefab, SlamSpawnPoint.position, Quaternion.identity);
        slam.SetActive(true);
        Destroy(slam, 5f);
    }
}
