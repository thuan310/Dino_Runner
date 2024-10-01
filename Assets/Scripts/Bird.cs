using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed = 7f;
    private bool soundPlayed;
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < 11f && !soundPlayed)
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.Bird);
            soundPlayed = true;
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
