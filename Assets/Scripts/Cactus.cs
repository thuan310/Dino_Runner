using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;


        if (transform.position.x < -10f) 
        {
            Destroy(gameObject); 
        }
    }
}
