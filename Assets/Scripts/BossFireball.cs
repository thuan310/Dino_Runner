using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : MonoBehaviour {


    [SerializeField] private float speed = 5f;  
    [SerializeField] private float homingDistance = 2f;

    private Transform player;
    private bool isHoming = true;  
    private Vector3 direction;

    void Start()
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.Shoot);

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the Player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (isHoming)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            transform.position += directionToPlayer * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
            if (Vector3.Distance(transform.position, player.transform.position) <= homingDistance)
            {
                isHoming = false;  
                direction = directionToPlayer;  
            }
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
