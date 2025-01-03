using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage = 20f; // Damage dealt to the player
    public Transform attackPoint; // Point of attack (e.g., claws or weapon tip)
    public float attackRange = 1f; // Attack range
    public LayerMask playerLayer; // Layer of the player
    public AudioSource hitSound;

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
}