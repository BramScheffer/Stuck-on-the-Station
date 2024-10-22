using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform trein; // The train target
    public float attackRange = 2.0f; // Range within which the zombie can attack
    public float attackDamage = 10f; // Damage dealt by the zombie
    public float slowedSpeed2 = 1.5f;
    public float slowDuration2 = 2f;

    public NavMeshAgent agent; // NavMeshAgent for movement
    private bool isAttacking = false; // Tracks whether the zombie is attacking
    private ZombieAnimator zombieAnimator; // Reference to the ZombieAnimator script
    public Transform currentTarget; // Current target for the zombie

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<ZombieAnimator>();
        agent.SetDestination(trein.position); // Set destination to the train
    }

    private void Update()
    {
        if (currentTarget != null && !isAttacking)
        {
            // Move towards the target
            agent.SetDestination(currentTarget.position);

            // Check if the zombie is within attack range
            if (IsWithinAttackRange())
            {
                Debug.Log("Zombie is within attack range.");
                StartCoroutine(Attack()); // Start the Attack coroutine
            }
        }
        else if (currentTarget == null)
        {
            UpdateTarget(); // Find a new target if none exists
        }
    }

    public void SetTarget(Transform target)
    {
        this.currentTarget = target;
    }

    private bool IsWithinAttackRange()
    {
        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            Debug.Log($"Distance to target: {distance}");
            return distance <= attackRange;
        }
        return false;
    }

    IEnumerator Attack()
    {
        isAttacking = true; // Mark as attacking
        agent.isStopped = true; // Stop movement
        Debug.Log("Zombie is attacking."); // Log dat de zombie aanvalt

        while (currentTarget != null && !currentTarget.GetComponent<Health>().IsDead())
        {
            Debug.Log("Triggering attack animation."); // Log dat de aanval animatie wordt getriggerd
            zombieAnimator.TriggerAttackAnimation(); // Trigger attack animation
            yield return new WaitForSeconds(0.5f); // Wait for animation delay
            BrengSchadeToe(); // Deal damage

            float attackAnimationLength = zombieAnimator.GetAttackAnimationLength();
            yield return new WaitForSeconds(attackAnimationLength - 0.5f); // Wait for remaining animation
        }

        if (currentTarget != null && currentTarget.GetComponent<Health>().IsDead())
        {
            UpdateTarget(); // Find a new target if the current is dead
        }

        isAttacking = false; // Reset attack status
        agent.isStopped = false; // Resume movement
    }



    public void BrengSchadeToe()
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.BrengSchadeToe(attackDamage); // Dit moet nu werken
                Debug.Log("Damage dealt to the target.");
            }
            else
            {
                Debug.LogWarning("Current target has no Health component.");
            }
        }
    }


    public void BarbedHit()
    {
        StartCoroutine(ApplySlowEffect());
    }

    IEnumerator ApplySlowEffect()
    {
        float originalSpeed = agent.speed;
        agent.speed = slowedSpeed2;

        yield return new WaitForSeconds(slowDuration2);

        agent.speed = originalSpeed;
    }

    private void UpdateTarget()
    {
        // Logic to find a new target
    }
}
