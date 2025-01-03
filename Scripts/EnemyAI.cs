using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player
    private NavMeshAgent agent;
    public Health health;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
       


            if(player != null && health.currentHealth > 0)
            {
                agent.SetDestination(player.position);
            }
        
    }
}
