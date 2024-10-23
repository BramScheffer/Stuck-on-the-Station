using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform train;  // Referentie naar de trein
    public float attackDistance = 2f;  // Afstand waarbij de zombie aanvalt
    public float damage = 10f;  // Schade per aanval
    public float attackRate = 2f;  // Tijd tussen aanvallen
    private float nextAttackTime = 0f;  // Volgende aanvalstijd
    public float slowedSpeed2 = 0f;  // Vertraagde snelheid
    public float slowDuration2 = 0f;  // Duur van de vertraging

    public NavMeshAgent agent;  // NavMeshAgent component voor navigatie
    private Animator animator;  // Animator component voor animaties
    private bool isAttacking = false;  // Of de zombie aan het aanvallen is

    // Voeg een variabele toe om te controleren of dit de kleine zombie is
    public bool isSmallZombie = false;

    private void Start()
    {
        // Referenties ophalen
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Voeg een debug om te checken welke zombie dit is
        if (isSmallZombie)
        {
            Debug.Log($"{gameObject.name} is a small zombie.");
        }
        else
        {
            Debug.Log($"{gameObject.name} is a big zombie.");
        }
    }

    private void Update()
    {
        // Bereken de afstand tot de trein
        float distanceToTrain = Vector3.Distance(transform.position, train.position);

        // Debug om te zien of de zombie dichtbij genoeg is om aan te vallen
        Debug.Log($"{gameObject.name} Distance to train: {distanceToTrain}");

        // Pas de attackDistance aan als dit een kleine zombie is
        if (isSmallZombie)
        {
            attackDistance = 5f;  // Maak de afstand kleiner voor de kleine zombie
            damage = 2f;  // Verlaag de schade voor kleine zombies
        }

        // Als de zombie dichtbij genoeg is, start de aanval
        if (distanceToTrain <= attackDistance)
        {
            agent.isStopped = true;  // Stop de zombie
            animator.SetBool("isWalking", false); // Stop wandel animatie

            // Alleen aanvallen als er genoeg tijd is verstreken
            if (Time.time >= nextAttackTime)
            {
                if (!isAttacking)
                {
                    StartAttack();  // Start de aanval animatie
                }

                // Check of de attack animatie klaar is
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1.0f)
                {
                    DealDamage();  // Doe schade aan het einde van de aanval
                    nextAttackTime = Time.time + 1f / attackRate;  // Volgende aanval
                    isAttacking = false;  // Reset de aanval status
                }
            }
        }
        else
        {
            // Blijf naar de trein lopen als we niet dichtbij genoeg zijn
            agent.isStopped = false;
            agent.SetDestination(train.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
            isAttacking = false; // Reset aanvallen
        }
    }

    private void StartAttack()
    {
        isAttacking = true; // Markeer als aanvallend
        animator.SetBool("isAttacking", true); // Trigger de aanval animatie
        Debug.Log($"{gameObject.name} started attacking!");
    }

    private void DealDamage()
    {
        // Breng schade toe aan de trein
        Health trainHealth = train.GetComponent<Health>();
        if (trainHealth != null && !trainHealth.IsDead())
        {
            trainHealth.BrengSchadeToe(damage); // Doe schade
            Debug.Log($"{gameObject.name} dealt {damage} damage!");

            // Optioneel: Als de trein vernietigd is, stop met aanvallen
            if (trainHealth.IsDead())
            {
                agent.isStopped = true;
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }
    }

    // Methode om het doel (de trein) in te stellen
    public void SetTarget(Transform target)
    {
        train = target;
    }

    // Vertraagd effect toepassen wanneer geraakt
    public void BarbedHit()
    {
        StartCoroutine(ApplySlowEffect());
    }

    private IEnumerator ApplySlowEffect()
    {
        float originalSpeed = agent.speed; // Sla originele snelheid op
        agent.speed = slowedSpeed2; // Verlaag snelheid

        yield return new WaitForSeconds(slowDuration2); // Wacht voor slow duration

        agent.speed = originalSpeed; // Herstel originele snelheid
    }
}
