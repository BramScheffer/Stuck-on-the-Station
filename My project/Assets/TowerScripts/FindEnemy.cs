using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    public Transform findEnemy;
    public LayerMask enemyLayerMask;
    public float radius;
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform FindEnem()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);
        Transform closestTarget = null;
        float maxDistance = radius;

        foreach (Collider enemyCollider in nearbyEnemies)
        {
            float enemyDistance = Vector3.Distance(enemyCollider.transform.position, transform.position);
            if (enemyDistance < maxDistance)
            {
                closestTarget = enemyCollider.transform;
                maxDistance = enemyDistance;
            }
        }

        if (nearbyEnemies.Length == 0)
        {
            maxDistance = radius;
            closestTarget = null;

        }
        return closestTarget;
    }
}
