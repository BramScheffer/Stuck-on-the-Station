using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player; // The Tower
    public List<Transform> turrets; // All Turrets in the level
    public float attackRange = 2.0f; // Range within which zombie attacks
    public float attackCooldown = 1.5f; // Time between attacks
    private float lastAttackTime;

    private NavMeshAgent agent;
    private Transform currentTarget; // The current target of the zombie

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateTarget(); // Find the first target
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            agent.destination = currentTarget.position;

            // Check if the zombie is within attack range of the current target
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                // Attack only if cooldown has passed
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    Attack();
                }
            }
        }
        else
        {
            // If no Turrets remain, attack the Tower
            UpdateTarget();
        }
    }

    void UpdateTarget()
    {
        // Find the closest Turret, if any exist
        currentTarget = GetClosestTurret();

        // If no Turrets remain, attack the Tower
        if (currentTarget == null)
        {
            currentTarget = player; // The Tower becomes the target
        }
    }

    Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        // Iterate through the list of turrets
        for (int i = turrets.Count - 1; i >= 0; i--)
        {
            Transform turret = turrets[i];

            if (turret != null) // Ensure the turret exists
            {
                float distanceToTurret = Vector3.Distance(transform.position, turret.position);
                if (distanceToTurret < closestDistance)
                {
                    closestDistance = distanceToTurret;
                    closestTurret = turret;
                }
            }
            else
            {
                // Remove null entries from the list (i.e., destroyed turrets)
                turrets.RemoveAt(i);
            }
        }

        return closestTurret;
    }

    void Attack()
    {
        // Register the time of this attack
        lastAttackTime = Time.time;

        // If the current target is a turret or the player, apply damage
        if (currentTarget != player && currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(25f); // Apply damage to the turret or player
            }

            // If the turret is destroyed (or health is <= 0), update the target
            if (targetHealth == null || targetHealth.IsDead())
            {
                turrets.Remove(currentTarget); // Remove destroyed turret from the list
                UpdateTarget(); // Update to the next closest turret or tower
            }
        }
    }


}
