using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimator : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private ZombieAI zombieAI;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        zombieAI = GetComponent<ZombieAI>();
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            SetWalking(true);
        }
        else
        {
            SetWalking(false);
        }

        // Only trigger attack if zombie is close enough and not already attacking
        if (zombieAI.currentTarget != null && zombieAI.IsWithinAttackRange() && !zombieAI.isAttacking)
        {
            SetAttack();
        }
    }

    // Trigger walking animation
    public void SetWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    // Trigger attack animation
    public void SetAttack()
    {
        animator.SetTrigger("isAttacking");
        zombieAI.StartAttack(); // Tell ZombieAI to start attack logic
    }

    // Called from animation event
    public void OnAttackHit()
    {
        zombieAI.ApplyDamage(); // Apply the damage during the impact frame of the animation
    }
}
