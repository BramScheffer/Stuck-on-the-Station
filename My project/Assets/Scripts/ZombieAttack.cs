using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float damage = 50f; // Damage dealt by the zombie per attack
    public float attackCooldown = 1.5f; // Time between attacks
    private float lastAttackTime = 0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Method to trigger the attack on the target
    public void TryAttack(Transform target)
    {
        // Only attack if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Trigger the attack animation
            animator.SetTrigger("Attack");

            // Apply damage to the target
            DealDamage(target);

            // Register the time of this attack
            lastAttackTime = Time.time;
        }
    }

    // This method applies damage to the target if it has a health system
    private void DealDamage(Transform target)
    {
        // Check if the target has a health system (e.g., a script named "Health")
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth != null)
        {
            // Apply damage to the target's health
            targetHealth.TakeDamage(damage);

            // Optional: Log the damage dealt
            Debug.Log($"Dealt {damage} damage to {target.name}");

            // If the target is destroyed after taking damage, remove it from the ZombieAI's target list (if it's a turret)
            if (targetHealth.IsDead())
            {
                ZombieAI zombieAI = GetComponent<ZombieAI>();
                if (zombieAI != null && zombieAI.turrets.Contains(target))
                {
                    zombieAI.turrets.Remove(target);
                }

                // Destroy the turret or handle death logic for the player
                Destroy(target.gameObject);
            }
        }
        else
        {
            Debug.LogWarning($"{target.name} does not have a Health component!");
        }
    }
}
