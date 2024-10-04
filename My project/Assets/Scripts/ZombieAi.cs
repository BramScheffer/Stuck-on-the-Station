using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform trein; // The Tower
    public List<Transform> turrets; // All Turrets in the level
    public float attackRange = 2.0f; // Range within which zombie attacks
    public float attackCooldown = 1.5f; // Time between attacks
    private float lastAttackTime;
    public float attackdamage;

    private NavMeshAgent agent;
    private Transform currentTarget; // The current target of the zombie
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateTarget(); // Find the first target
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            agent.destination = currentTarget.position;

            // Check if the zombie is within attack range of the current target
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                // Attack only if cooldown has passed
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    Attack();
                }
            }
        }
        else
        {
            // If no Turrets remain, attack the Tower
            UpdateTarget();
        }
    }

    void UpdateTarget()
    {
        // Find the closest Turret, if any exist
        currentTarget = GetClosestTurret();

        // If no Turrets remain, attack the Tower
        if (currentTarget == null)
        {
            currentTarget = trein; // The Tower becomes the target
        }
    }

    Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        // Iterate through the list of turrets
        for (int i = turrets.Count - 1; i >= 0; i--)
        {
            Transform turret = turrets[i];

            if (turret != null) // Ensure the turret exists
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
                // Remove null entries from the list (i.e., destroyed turrets)
                turrets.RemoveAt(i);
            }
        }

        return closestTurret;
    }

    void Attack()
    {
        // Registreer het moment van deze aanval
        lastAttackTime = Time.time;

        // Controleer of het huidige doelwit de trein (toren) is
        if (currentTarget == trein)
        {
            Debug.Log("Probeer de toren aan te vallen...");
            Health towerHealth = trein.GetComponent<Health>();
            if (towerHealth != null)
            {
                Debug.Log("De toren neemt schade!");
                towerHealth.TakeDamage(attackdamage); // Schade toebrengen aan de toren
            }
        }
        // Als het huidige doelwit een turret is
        else if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                Debug.Log("Aanval op een turret!");
                targetHealth.TakeDamage(attackdamage); // Schade toebrengen aan de turret
            }

            // Als de turret vernietigd is, update het doelwit
            if (targetHealth == null || targetHealth.IsDead())
            {
                turrets.Remove(currentTarget); // Verwijder de vernietigde turret
                UpdateTarget(); // Werk het doelwit bij
            }
        }
    }

}
