using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float speed = 10f;
    private bool soundPlayed = false;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < 8f)
        {
            GetComponentInChildren<Animator>().SetTrigger("playBeam");
        }

        if (transform.position.x < 7f && !soundPlayed)
        {
            SoundManager.instance.PlaySound(SoundManager.Sound.UFO);
            soundPlayed = true;
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
