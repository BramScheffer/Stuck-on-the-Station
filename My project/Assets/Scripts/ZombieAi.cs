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
        // Als er een huidig doel is en de zombie is niet aan het aanvallen
        if (currentTarget != null && !isAttacking)
        {
            agent.SetDestination(currentTarget.position);

            // Als de zombie binnen het aanvalbereik is, start de aanval
            if (IsWithinAttackRange())
            {
                StartCoroutine(Attack()); // Start de Attack coroutine
            }
        }

        // Als er geen huidig doel is, update het doel
        if (currentTarget == null)
        {
            UpdateTarget();
        }
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
    // Methode om schade toe te brengen aan het huidige doelwit
    public void BrengSchadeToe()
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.BrengSchadeToe(attackDamage); // Toepassen van schade op het doelwit

                // Check of het doelwit dood is na schade toebrengen
                if (targetHealth.IsDead())
                {
                    Debug.Log($"{currentTarget.name} is vernietigd door de zombie!");

                    // Verwijder het doelwit uit de lijst van turrets
                    turrets.Remove(currentTarget);

                    currentTarget = null; // Reset het huidige doelwit
                    UpdateTarget(); // Update het doelwit naar een ander turret of trein
                }
            }
            else
            {
                Debug.LogWarning("Het huidige doelwit heeft geen Health-component.");
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
    IEnumerator Attack()
    {
        // Zolang het huidige doel niet kapot is en het doel nog bestaat
        while (currentTarget != null && !currentTarget.GetComponent<Health>().IsDead())
        {
            // Stop de zombie om te kunnen aanvallen
            agent.isStopped = true;

            // Speel de aanvalsanimering af
            zombieAnimator.TriggerAttackAnimation();

            // Wacht totdat de aanvalsanimering bijna klaar is, bijvoorbeeld op het moment dat de schade plaatsvindt
            yield return new WaitForSeconds(0.5f); // Halverwege de animatie

            // Breng schade toe aan het doel
            BrengSchadeToe();

            // Wacht totdat de volledige aanvalsanimering klaar is voordat je opnieuw slaat
            float attackAnimationLength = zombieAnimator.GetAttackAnimationLength();
            yield return new WaitForSeconds(attackAnimationLength - 0.5f); // Wacht de rest van de animatie af
        }

        // Als het doelwit dood is, zoek een nieuw doel
        if (currentTarget != null && currentTarget.GetComponent<Health>().IsDead())
        {
            if (currentTarget != trein)
            {
                turrets.Remove(currentTarget); // Verwijder het kapotte doel uit de lijst
            }
            UpdateTarget(); // Zoek een nieuw doel
        }

        // Laat de zombie weer bewegen
        isAttacking = false;
        agent.isStopped = false;
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
