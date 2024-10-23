using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform train;  // Referentie naar de trein
    public float attackDistance = 2f;  // Afstand waarbij de zombie aanvalt
    public float damage = 10f;  // Schade per aanval
    public float slowedSpeed2 = 0f;  // Vertraagde snelheid
    public float slowDuration2 = 0f;  // Duur van de vertraging

    public NavMeshAgent agent;  // NavMeshAgent component voor navigatie
    private Animator animator;  // Animator component voor animaties
    private bool isAttacking = false;  // Controleer of de zombie aanvalt
    public float attackCooldown = 1.5f; // Tijd tussen aanvallen
    private float lastAttackTime = 0f; // Tijd van de laatste aanval

    void Start()
    {
        // Referenties ophalen
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Bereken de afstand tot de trein
        float distanceToTrain = Vector3.Distance(transform.position, train.position);
        Debug.Log($"Afstand tot de trein: {distanceToTrain}"); // Debug: afstand

        // Controleer of de trein nog leeft
        Health trainHealth = train.GetComponent<Health>();
        if (trainHealth == null || trainHealth.IsDead())
        {
            // Stop de zombie als de trein dood is
            animator.SetBool("isAttacking", false);
            isAttacking = false;
            agent.isStopped = true;
            return; // Stop de Update functie
        }

        // Als de zombie dichtbij genoeg is, stop met lopen en start met aanvallen
        if (distanceToTrain <= attackDistance)
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);

            // Start de aanval als de zombie niet al aanvalt
            if (!isAttacking)
            {
                // Start de aanval
                animator.SetBool("isAttacking", true);
                isAttacking = true;  // Update de aanvalstatus
                lastAttackTime = Time.time; // Reset de laatste aanvalstijd
                Debug.Log("Zombie start aanval"); // Debug: start aanval
            }

            // Controleer of de attack animatie actief is
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Attack"))
            {
                // Controleer of de aanvalstijd is verstreken
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    // Breng schade toe aan de trein als de aanvalsan animatie klaar is
                    DealDamage();
                    lastAttackTime = Time.time; // Update de tijd van de laatste aanval
                    Debug.Log("Zombie heeft schade toegebracht aan de trein"); // Debug: schade toebrengen
                }
            }
            else
            {
                // Reset de aanvallsituatie als de animatie is afgelopen
                if (stateInfo.normalizedTime >= 1.0f) // Check of animatie is afgelopen
                {
                    animator.SetBool("isAttacking", false);
                    isAttacking = false; // Reset de aanvallsituatie
                }
            }
        }
        else
        {
            // Als de zombie niet aanvalt, laat hem lopen richting de trein
            agent.isStopped = false;
            agent.SetDestination(train.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }

    void DealDamage()
    {
        // Schade toebrengen aan de trein
        Health trainHealth = train.GetComponent<Health>();
        if (trainHealth != null && !trainHealth.IsDead())
        {
            trainHealth.BrengSchadeToe(damage);
        }
    }

    // Methode om het doel (de trein) in te stellen
    public void SetTarget(Transform target)
    {
        train = target;
    }

    // Functie om een vertraagd effect toe te passen als de zombie door prikkeldraad (bijvoorbeeld) is geraakt
    public void BarbedHit()
    {
        StartCoroutine(ApplySlowEffect());
    }

    IEnumerator ApplySlowEffect()
    {
        // Sla de originele snelheid op
        float originalSpeed = agent.speed;
        // Stel de nieuwe vertraagde snelheid in
        agent.speed = slowedSpeed2;

        // Wacht gedurende de duur van de vertraging
        yield return new WaitForSeconds(slowDuration2);

        // Herstel de originele snelheid
        agent.speed = originalSpeed;
    }
}
