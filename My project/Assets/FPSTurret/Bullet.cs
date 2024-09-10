using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Time in seconds before the bullet is destroyed
    public float lifespan = 3f;

    void Start()
    {
        // Destroy the bullet after 'lifespan' seconds
        Destroy(gameObject, lifespan);
    }
}