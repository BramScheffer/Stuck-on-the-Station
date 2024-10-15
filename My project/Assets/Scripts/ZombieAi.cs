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
                StartCoroutine(Attack());
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

    // Coroutine om de aanval te beheren
    private IEnumerator Attack()
    {
        isAttacking = true;
        agent.isStopped = true; // Stop de zombie tijdens de aanval
        zombieAnimator.TriggerAttackAnimation(); // Start de aanvalsanimering

        // Wacht totdat de animatie een bepaalde tijd duurt (de duur van de aanvalsanimeer)
        yield return new WaitForSeconds(zombieAnimator.GetAttackAnimationLength());

        BrengSchadeToe(); // Breng schade toe na de animatie

        agent.isStopped = false; // Laat de zombie weer bewegen
        isAttacking = false; // Reset de aanvalseis
    }

    // Deze functie wordt aangeroepen om schade toe te brengen
    public void BrengSchadeToe()
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.BrengSchadeToe(attackDamage); // Breng de juiste schade toe
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
        // Bewaar de oorspronkelijke snelheid
        float originalSpeed = agent.speed;

        // Pas de vertraagde snelheid toe
        agent.speed = slowedSpeed2;

        // Wacht de duur van het vertraagde effect
        yield return new WaitForSeconds(slowDuration2);

        // Reset de snelheid terug naar de oorspronkelijke snelheid
        agent.speed = originalSpeed;
    }
}
