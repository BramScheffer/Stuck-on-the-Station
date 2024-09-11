using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int hits;
    public int health;
    public bool giant;
    public Event money;
    // Start is called before the first frame update
    void Start()
    {
        if (giant!)
        {
            health = 150;
        }
        else
        {
            health = 75;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print (hits);
    }

    public void hit()
    {
        hits += 1;
        health -= 5;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
