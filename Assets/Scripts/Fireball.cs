using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;


        if (transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.Explosion);
            Destroy(collision.gameObject);


            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);            
            
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.Explosion);

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            Boss boss = collision.GetComponentInParent<Boss>();
            boss.ReduceBossDuration();
            Destroy(gameObject);
        }
    }


}
