using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivateCollision : MonoBehaviour
{
    [SerializeField] private GameObject fireBreath;

    private void ActivateCollision()
    {
        fireBreath.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }
    private void DeactivateCollision()
    {
        fireBreath.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
}
