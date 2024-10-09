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
    private float lastAttackTime;
    public float attackdamage;

    public float speed = 3.5f;

    private NavMeshAgent agent;
    private Transform currentTarget;

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
                if (Time.time > lastAttackTime + attackCooldown)
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
                turrets.Remove(currentTarget);
                UpdateTarget();
            }
        }
    }
}
