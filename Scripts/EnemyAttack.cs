using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 20f; // Damage dealt to the player
    public Transform attackPoint; // Point of attack (e.g., claws or weapon tip)
    public float attackRange = 1f; // Attack range
    public LayerMask playerLayer; // Layer of the player
    public AudioSource hitSound;
    public GameObject player;
    public bool enemyGrappleMadeInd;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // This function will be triggered by the Animation Event
    public void PerformAttack()
    {
        // Detect player in range of attack
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        

        foreach (Collider player in hitPlayers)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                hitSound.Play();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }


    public void GrappleAttack()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        bool grappleMade = hitPlayers.Length > 0;
        
        if (grappleMade)
        {
            AnimationEvents.TriggerGrapple(true); // Notify all listeners (like player)
            enemyGrappleMadeInd= true;
            Debug.Log("enemyGrappleMadeInd: " + enemyGrappleMadeInd);

        }
    }
    /*public void GrappleAttack(bool grappleMade)
    {
        // Detect player in range of attack
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        grappleMade = false;

        foreach (Collider player in hitPlayers)
        {
            grappleMade = true;
            /* Health playerHealth = player.GetComponent<Health>();
             if (playerHealth != null)
             {
                 playerHealth.TakeDamage(attackDamage);
                 hitSound.Play();
             }
            
        }
        OnGrappleMade?.Invoke(grappleMade);
    }*/

    public void GrappleAnimTrigger()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        /*foreach(Collider player in hitPlayers)
        {
            if(player.GetComponent<Animator>() != null)
            {
                player.GetComponent<Animator>().SetBool("Grappled", true);
            }
        }*/
    }

    public void GrappleAnimStop()
    {
        /*if(player.GetComponent<CharacterAnimator>().escGrappleCnt > 8)
        {
            GetComponent<Animator>().SetTrigger("Grapple");
        }*/
        
        
    }
}