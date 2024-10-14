using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    private Animator animator; // Referentie naar de Animator
    private ZombieAI zombieAI; // Referentie naar de ZombieAI

    private void Start()
    {
        animator = GetComponent<Animator>();
        zombieAI = GetComponent<ZombieAI>();
    }

    private void Update()
    {
        // Als de zombie loopt, speel de loopanimatie
        if (zombieAI.agent.velocity.magnitude > 0.1f)
        {
            SetWalking(true);
        }
        else
        {
            SetWalking(false);
        }
    }

    // Start de aanvalsanimering
    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    // Zet de loopanimatie aan of uit
    public void SetWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    // Deze functie wordt aangeroepen door een Animation Event op het moment dat de aanval schade moet doen
    public void OnAttackHit()
    {
        zombieAI.ApplyDamage(); // Roep de ApplyDamage functie aan in ZombieAI
    }

    // Deze functie wordt aangeroepen aan het einde van de aanvalanimatie
    public void OnAttackEnd()
    {
        zombieAI.EndAttack(); // Eindig de aanval
    }
}
