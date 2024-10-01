using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private Vector3 startPos;

    [SerializeField] private float parallaxCo;
    [SerializeField] static float speed = 10f;

    private void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        Move();

    }

    private void Move()
    {
        float newPosition = Mathf.Repeat(Time.time * speed * parallaxCo, length);
        transform.position = startPos + Vector3.left * newPosition;
    }
}
