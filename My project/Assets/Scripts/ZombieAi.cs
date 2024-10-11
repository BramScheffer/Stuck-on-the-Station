using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform trein;
    public List<Transform> turrets;
    public float attackRange = 2.0f;
    public float attackCooldown = 1.5f;
    public float lastAttackTime;
    public float attackdamage;
    public float speed = 3.5f;
    public float slowedSpeed = 1.5f; // Speed when hit by barbed trap
    public float slowDuration = 3f; // Duration for slow effect

    public NavMeshAgent agent;
    public Transform currentTarget;
 
    public bool isAttacking = false; // Nieuwe variabele om bij te houden of de zombie aanvalt

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        UpdateTarget();
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            agent.destination = currentTarget.position;

            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                if (!isAttacking && Time.time > lastAttackTime + attackCooldown) // Controleer of niet al aanvalt
                {
                    Attack();
                }
            }
        }
        else
        {
            UpdateTarget();
        }
    }

    void UpdateTarget()
    {
        currentTarget = GetClosestTurret();

        if (currentTarget == null)
        {
            currentTarget = trein;
        }
    }

    Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        for (int i = turrets.Count - 1; i >= 0; i--)
        {
            Transform turret = turrets[i];

            if (turret != null)
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
                turrets.RemoveAt(i);
            }
        }

        return closestTurret;
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        isAttacking = true; // Markeer dat we aan het aanvallen zijn

        if (currentTarget == trein)
        {
            Health towerHealth = trein.GetComponent<Health>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(attackdamage);
            }
        }
        else if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackdamage);
            }

            if (targetHealth == null || targetHealth.IsDead())
            {
                turrets.Remove(currentTarget); // Verwijder de vernietigde turret
                UpdateTarget(); // Werk het doelwit bij naar het volgende doelwit
            }
        }

        // Zet isAttacking weer op false na de aanval, zodat de animatie opnieuw kan worden gespeeld
        Invoke("ResetAttack", 0.1f); // Wacht even voordat we weer kunnen aanvallen
    }

    private void ResetAttack()
    {
        isAttacking = false; // Reset de aanvalstatus
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
        agent.speed = slowedSpeed;

        // Wait for the duration of the slow effect
        yield return new WaitForSeconds(slowDuration);

        // Reset the speed back to the original speed
        agent.speed = originalSpeed;
    }
}

