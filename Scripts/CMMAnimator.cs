using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMMAnimator : MonoBehaviour
{
    public Transform player; // Reference to the player
    public NavMeshAgent agent; // Reference to NavMeshAgent
    private Animator animator; // Reference to Animator
    public Health health;

    public float detectionRange = 10.0f; // Maximum distance for detection
    public float attackRange = 1.0f; // Range within which the enemy can attack
    private float attackCooldown = 3.0f; // Cooldown time for attacks
    private float lastAttackTime = -Mathf.Infinity; // Tracks the last attack time

    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
                


        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            
            float cmmSpeed = agent.velocity.magnitude;
            animator.SetFloat("CMMMove", cmmSpeed);
            animator.SetFloat("CMMHealth",health.currentHealth);


            // Check if the player is within detection range
            if (distanceToPlayer <= detectionRange && health.currentHealth >0)
            {
                // Start following the player
                agent.isStopped = false; // Ensure the agent is active
                agent.SetDestination(player.position);

                // Determine if the agent is moving
               // bool isMoving = agent.remainingDistance > agent.stoppingDistance;
                
               // animator.SetBool("moving", isMoving);

                // Handle attacking if within attack range
                if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetTrigger("attack");
                    lastAttackTime = Time.time; // Update the last attack time
                }

                
            }
            else
            {
                // Stop the enemy when out of detection range
                agent.isStopped = true; // Stops the agent completely
                //agent.ResetPath();
               // animator.SetBool("moving", false); // Transition to idle animation
            }


            health.OnTakeDamage += HandleTakeDamage;

        }
    }

    void HandleTakeDamage(float damage)
    {
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }
    }

}