using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    

    private void Start()
    {
        
    }
    public void SetDamage(float value)
    {
        damage = value;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the hit object has a health component
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            
        }
        
        // Destroy the projectile on impact
        Destroy(gameObject);
        
    }
}
