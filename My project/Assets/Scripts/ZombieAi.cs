using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player; // Dit is waarschijnlijk de Tower
    public List<Transform> turrets; // Alle Turrets in het level
    private NavMeshAgent agent;
    private Animator animator;
    public float attackRange = 2f;
    public bool geraakt;

    private Transform currentTarget; // Het huidige doel van de zombie

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        UpdateTarget(); // Zoek het eerste doel
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            agent.destination = currentTarget.position;

            // Controleer of de zombie binnen aanvalsbereik is van het huidige doel
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            // Als er geen Turrets meer zijn, val de Tower aan
            UpdateTarget();
        }
    }

    void UpdateTarget()
    {
        // Zoek de dichtstbijzijnde Turret, als die er is
        currentTarget = GetClosestTurret();

        // Als er geen Turrets meer zijn, val de Tower aan
        if (currentTarget == null)
        {
            currentTarget = player; // De Tower wordt het doel
        }
    }

    Transform GetClosestTurret()
    {
        Transform closestTurret = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform turret in turrets)
        {
            if (turret != null) // Zorg ervoor dat de Turret bestaat
            {
                float distanceToTurret = Vector3.Distance(transform.position, turret.position);
                if (distanceToTurret < closestDistance)
                {
                    closestDistance = distanceToTurret;
                    closestTurret = turret;
                }
            }
        }

        return closestTurret;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        // Als de huidige target een Turret is, controleer of die vernietigd moet worden
        if (currentTarget != player && currentTarget != null)
        {
            // Vernietig de turret of doe schade (afhankelijk van je gameplay systeem)
            Destroy(currentTarget.gameObject);

            // Update het doel naar de volgende Turret of Tower
            UpdateTarget();
        }
    }
}
