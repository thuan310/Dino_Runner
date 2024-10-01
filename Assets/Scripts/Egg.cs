using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EggType {
    Jump,
    Shield,
    Shoot
}

public class Egg : MonoBehaviour
{
    public EggType eggType;
    public float speed = 10f;
    private bool canMove = true;
    private bool soundPlayed = true;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;


            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
            }

            if (transform.position.x < 6f && !soundPlayed)
            {
                SoundManager.instance.PlaySound(SoundManager.Sound.Egg);
                soundPlayed = true;
            }
        }
    }

    public void PlayHatchAnimation()
    {
        transform.GetComponentInChildren<Animator>().SetTrigger("hatch");

    }

    public void PlayDeathAnimation()
    {
        transform.GetComponentInChildren<Animator>().SetTrigger("hurt");
    }

    public void StopMove()
    {
        canMove = false;
    }
}
