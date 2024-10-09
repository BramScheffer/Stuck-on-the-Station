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

        if (zombieAI.currentTarget != null &&
            Vector3.Distance(transform.position, zombieAI.currentTarget.position) <= zombieAI.attackRange)
        {
            if (Time.time > zombieAI.lastAttackTime + zombieAI.attackCooldown)
            {
                SetAttack();
            }
        }
    }

    private void SetWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    private void SetAttack()
    {
        animator.SetTrigger("isAttacking");
    }
}
