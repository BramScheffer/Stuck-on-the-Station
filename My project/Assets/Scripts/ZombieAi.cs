using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform trein; // The train target
    public List<Transform> turrets; // List of turrets to attack
    public float attackRange = 2.0f; // Range within which to attack
    public float attackDamage; // Amount of damage dealt per attack
    public float speed = 3.5f; // Normal movement speed
    public float slowedSpeed2 = 2f; // Speed when slowed
    public float slowDuration2 = 3f; // Duration of slow effect

    public NavMeshAgent agent; // Reference to the NavMeshAgent
    public Transform currentTarget; // The current target the zombie is attacking

    public bool isAttacking = false; // To check if the zombie is currently attacking

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        UpdateTarget(); // Set the initial target
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            agent.destination = currentTarget.position;

            // If not attacking and within range, start attack
            if (!isAttacking && IsWithinAttackRange())
            {
                StartAttack(); // Start the attack
            }
        }
        else
        {
            UpdateTarget(); // Update target if none is set
        }
    }

    // Check if the zombie is within attack range
    public bool IsWithinAttackRange()
    {
        return currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackRange;
    }

    // Start the attack
    public void StartAttack()
    {
        isAttacking = true; // Mark as attacking
        // Trigger the attack animation here, if necessary
    }

    // Called by animation event to apply damage
    public void ApplyDamage()
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage); // Deal damage
                if (targetHealth.IsDead())
                {
                    turrets.Remove(currentTarget); // Remove dead turret
                    UpdateTarget(); // Update the target to the next one
                }
            }
        }

        EndAttack(); // End the attack after applying damage
    }

    // End the attack
    private void EndAttack()
    {
        isAttacking = false; // Reset attacking status
    }

    // Update the target to the closest turret or train
    void UpdateTarget()
    {
        currentTarget = GetClosestTurret() ?? trein; // Set target to the closest turret or train
    }

    // Find the closest turret
    Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.position);
            if (distanceToTurret < closestDistance)
            {
                closestDistance = distanceToTurret; // Update closest distance
                closestTurret = turret; // Update closest turret
            }
        }

        return closestTurret; // Return the closest turret
    }

    // Called when the zombie is hit by a barbed trap
    public void BarbedHit()
    {
        StartCoroutine(ApplySlowEffect()); // Start slow effect coroutine
    }

    // Coroutine to apply slow effect
    IEnumerator ApplySlowEffect()
    {
        float originalSpeed = agent.speed; // Store original speed
        agent.speed = slowedSpeed2; // Apply slowed speed
        yield return new WaitForSeconds(slowDuration2); // Wait for slow duration
        agent.speed = originalSpeed; // Reset speed to original
    }
}
