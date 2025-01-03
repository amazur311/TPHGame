using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public NavMeshAgent agent;
    private TextMeshProUGUI playerHealthText;
    public event Action<float> OnTakeDamage;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health

        if (gameObject.CompareTag("Player"))
        {
            playerHealthText = GameObject.Find("PlayerHealth").GetComponent<TextMeshProUGUI>();
            playerHealthText.SetText("Player Health: " + currentHealth);
        }
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (gameObject.CompareTag("Player"))
        {
            playerHealthText.SetText("Player Health: " + currentHealth);
        }
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {currentHealth}");
        OnTakeDamage?.Invoke(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle death logic
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        if(agent != null)
        {
            agent.isStopped = true;
            //agent.enabled = false;
        }
        // Add death animation or effects here
        // Remove object from the scene (customize for enemies vs. player)
    }
}
