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

    // Deze functie retourneert de lengte van de aanvalsanimering
    public float GetAttackAnimationLength()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "Attack") // Zorg ervoor dat de naam overeenkomt met je animatie
            {
                return clip.length;
            }
        }
        return 0f; // Standaard waarde als de animatie niet gevonden is
    }

    // Deze functie wordt aangeroepen door een Animation Event op het moment dat de aanval schade moet doen
    public void OnAttackHit()
    {
        zombieAI.BrengSchadeToe(); // Correcte aanroep van de schade methode
    }

    // Deze functie wordt aangeroepen aan het einde van de aanvalsanimering
    public void OnAttackEnd()
    {
        // Hier is geen logica meer nodig omdat we nu alles in de coroutine beheren
    }
}
