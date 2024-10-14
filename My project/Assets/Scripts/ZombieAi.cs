using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform trein; // Het treindoel
    public List<Transform> turrets; // Lijst met turrets om aan te vallen
    public float attackRange = 2.0f; // Bereik waarin de zombie kan aanvallen
    public float attackDamage = 10f; // Hoeveel schade de zombie aanricht per aanval
    public float slowedSpeed2 = 1.5f;
    public float slowDuration2 = 2f;

    public NavMeshAgent agent; // NavMeshAgent voor beweging
    public Transform currentTarget; // Huidig doelwit (turret of trein)

    private bool isAttacking = false; // Houdt bij of de zombie aanvalt
    private ZombieAnimator zombieAnimator; // Referentie naar het ZombieAnimator-script

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<ZombieAnimator>(); // Haal het ZombieAnimator-script op
        UpdateTarget(); // Vind het eerste doelwit
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
                StartAttack();
            }
        }

        // Als er geen huidig doel is, update het doel
        if (currentTarget == null)
        {
            UpdateTarget();
        }
    }

    // Controleer of de zombie binnen het aanvalbereik is
    private bool IsWithinAttackRange()
    {
        return Vector3.Distance(transform.position, currentTarget.position) <= attackRange;
    }

    // Start de aanval
    private void StartAttack()
    {
        isAttacking = true;
        agent.isStopped = true; // Stop de zombie tijdens de aanval
        zombieAnimator.TriggerAttackAnimation(); // Start de aanvalsanimering
    }

    // Wordt door de animatie aangeroepen (Animation Event) om schade toe te brengen
    public void ApplyDamage()
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);

                if (targetHealth.IsDead())
                {
                    // Verwijder het doel als het dood is
                    if (currentTarget != trein)
                    {
                        turrets.Remove(currentTarget);
                    }
                    UpdateTarget(); // Zoek een nieuw doelwit
                }
            }
        }
    }

    // Eindig de aanval en laat de zombie weer bewegen
    public void EndAttack()
    {
        isAttacking = false;
        agent.isStopped = false; // Laat de zombie weer bewegen
    }

    // Update het doel naar de dichtstbijzijnde turret of de trein
    private void UpdateTarget()
    {
        currentTarget = GetClosestTurret() ?? trein; // Vind de dichtstbijzijnde turret, of ga naar de trein als er geen turrets zijn
    }

    // Vind de dichtstbijzijnde turret
    private Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.position);
            if (distanceToTurret < closestDistance)
            {
                closestDistance = distanceToTurret;
                closestTurret = turret;
            }
        }

        return closestTurret;
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

        // Reset the speed back to the original speed
        agent.speed = originalSpeed;
    }
}
