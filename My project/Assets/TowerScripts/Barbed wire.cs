using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbedwire : MonoBehaviour
{
    // This method is called when another collider makes contact with this object.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object that collided with this has the layer "Enemy"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Print "Hello" if the object that collided has the "Enemy" layer
          ZombieAI enemy = collision.gameObject.GetComponent<ZombieAI>();
            if (enemy != null)
            {
                enemy.BarbedHit();
            }
            else 
            {
                Debug.Log("sigmagoon");
            }
        }
    }
}
