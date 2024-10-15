using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform trein; // The train target
    public List<Transform> turrets; // List of turrets to attack
    public float attackRange = 2.0f; // Range within which the zombie can attack
    public float attackDamage = 10f; // Damage dealt by the zombie
    public float slowedSpeed2 = 1.5f;
    public float slowDuration2 = 2f;

    public NavMeshAgent agent; // NavMeshAgent for movement
    public Transform currentTarget; // Current target (turret or train)

    private bool isAttacking = false; // Tracks whether the zombie is attacking
    private ZombieAnimator zombieAnimator; // Reference to the ZombieAnimator script

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<ZombieAnimator>(); // Get the ZombieAnimator script
        UpdateTarget(); // Find the first target
    }

    private void Update()
    {
        // If there is a current target and the zombie is not attacking
        if (currentTarget != null)
        {
            if (!isAttacking)
            {
                agent.SetDestination(currentTarget.position); // Move towards the target

                // If the zombie is within attack range, start the attack
                if (IsWithinAttackRange())
                {
                    StartAttack();
                }
            }
        }
        else // If there is no current target, update the target
        {
            UpdateTarget();
        }

        // Check if the agent is still moving to update animations
        UpdateAnimation();
    }

    // Check if the zombie is within attack range
    private bool IsWithinAttackRange()
    {
        return currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackRange;
    }

    // Start the attack
    private void StartAttack()
    {
        isAttacking = true;
        agent.isStopped = true; // Stop the zombie during the attack
        zombieAnimator.TriggerAttackAnimation(); // Start the attack animation
    }

    // Method to apply damage, called by the animator
    public void BrengSchadeToe()
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.BrengSchadeToe(attackDamage); // Apply the correct damage
                if (targetHealth.IsDead())
                {
                    // Remove the target if it is dead
                    turrets.Remove(currentTarget); // Remove it from the list
                    currentTarget = null; // Set current target to null
                    UpdateTarget(); // Find a new target
                }
            }
        }
    }

    // End the attack and let the zombie move again
    public void EindigAanval()
    {
        isAttacking = false;
        agent.isStopped = false; // Allow the zombie to move again
    }

    // Update the target to the nearest turret or the train
    private void UpdateTarget()
    {
        // Remove any null entries from the turrets list
        turrets.RemoveAll(t => t == null);

        // Get the closest turret
        currentTarget = GetClosestTurret() ?? trein; // Find the closest turret, or go to the train if there are no turrets

        // If there's a new target, set the agent's destination
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position); // Move to the new target
            Debug.Log($"New Target Set: {currentTarget.name}, Position: {currentTarget.position}");
        }
        else
        {
            Debug.Log("No target found, moving to default position.");
        }
    }

    // Find the closest turret
    private Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform turret in turrets)
        {
            if (turret == null) continue; // Skip destroyed turrets

            float distanceToTurret = Vector3.Distance(transform.position, turret.position);
            if (distanceToTurret < closestDistance)
            {
                closestDistance = distanceToTurret;
                closestTurret = turret;
            }
        }

        return closestTurret;
    }

    // Update animation state based on movement
    private void UpdateAnimation()
    {
        // Check if the zombie is moving
        if (agent.velocity.magnitude > 0.1f)
        {
            zombieAnimator.SetWalking(true); // Set walking animation
        }
        else
        {
            zombieAnimator.SetWalking(false); // Stop walking animation
        }
    }

    public void BarbedHit()
    {
        StartCoroutine(ApplySlowEffect());
    }

    IEnumerator ApplySlowEffect()
    {
        // Store the original speed
        float originalSpeed = agent.speed;

        // Apply the slowed speed
        agent.speed = slowedSpeed2;

        // Wait for the duration of the slow effect
        yield return new WaitForSeconds(slowDuration2);

        // Reset the speed back to the original
        agent.speed = originalSpeed;
    }
}
