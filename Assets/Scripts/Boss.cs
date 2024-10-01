using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private float minAttackInterval = 2f;
    [SerializeField] private float maxAttackInterval = 3f;
    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private BossFireball fireballPrefab;

    [SerializeField] private float bossDuration = 10f;
    [SerializeField] private float hitReduceAmount = 1f;
    private float attackTimer;

    private bool isAlive = true;
    private void Awake()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Roar);

        animator = GetComponentInChildren<Animator>();
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }


    void Update()
    {
        Move();
        Attack();
        
        CountdownBossDuration();
    }

    private void Move()
    {

        if (transform.position.x > 8f)
        {
            transform.position += Vector3.left * 5 * Time.deltaTime;
        }
    }


    private void Attack()
    {

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && isAlive)
        {
                
            int attackType = Random.Range(1, 4);

            switch (attackType)
            {
                case 1:
                    Fire();
                    break;
                case 2:
                    Breath();
                    break;
                case 3:
                    Pounce();
                    break;
            }

            attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
        }
        
    }

    private void Fire()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Shoot);
        animator.SetTrigger("fire");
        BossFireball fireball = Instantiate(fireballPrefab, fireBallSpawnPoint.position, Quaternion.identity) ;
        fireball.gameObject.SetActive(true);
    }

    private void Breath()
    {
        animator.SetTrigger("breath");
    }

    private void Pounce()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Prepare);
        animator.SetTrigger("pounce");
    }

    private void CountdownBossDuration()
    {
        if (!isAlive) return;

        bossDuration -= Time.deltaTime;
        if (bossDuration <= 0f)
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.Roar);
            transform.GetComponentInChildren<Animator>().SetTrigger("dead");
            if (isAlive)
            {
                SoundManager.instance.PlaySound(SoundManager.Sound.Roar);
                isAlive = false;
            }
            Destroy(gameObject, 1.5f); 
        }

    }

    public void ReduceBossDuration()
    {
        if (!isAlive) return;

        bossDuration -= hitReduceAmount;
        if (bossDuration <= 0f)
        {
            transform.GetComponentInChildren<Animator>().SetTrigger("dead");
            Destroy(gameObject, 1.5f);
            if (isAlive)
            {
                SoundManager.instance.PlaySound(SoundManager.Sound.Roar);
                isAlive = false;
            }
        }
    }
}
