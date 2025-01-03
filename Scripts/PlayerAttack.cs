using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float meleeDamage = 25f; // Damage dealt by melee attacks
    public float meleeRange = 2f; // Range for melee attacks
    public Transform meleeAttackPoint; // Point where melee damage is calculated
    public int bulletCnt;
    public int pistolMag = 8;
    public int rifleMag = 1;
    public int shotgunMag = 2;

    public GameObject rangedProjectilePrefab; // Prefab for the ranged projectile
    public Transform rangedAttackPoint; // Point where projectiles are fired
    public float rangedDamage = 20f; // Damage dealt by ranged projectiles
    public float rangedAttackForce = 20f; // Speed of the projectile
    public AudioSource hitSound;
    public AudioSource pistolSound;
    public AudioSource emptyMagSound;
    public AudioSource reloadMagSound;

    public Camera playerCamera; // Reference to the player camera for aiming
    public LayerMask enemyLayer; // Layer for detecting enemies

    // Melee attack logic triggered from an animation event
    public void PerformMeleeAttack()
    {
        // Detect enemies in the melee attack range
        Collider[] hitEnemies = Physics.OverlapSphere(meleeAttackPoint.position, meleeRange, enemyLayer);

        // Apply damage to each enemy hit
        foreach (Collider enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(meleeDamage);
                hitSound.Play();
            }
        }
    }

    // Ranged attack logic triggered from an animation event
    public void PerformRangedAttack()
    {
        if (bulletCnt > 0)
        {
            bulletCnt--;
            pistolSound.Play();

            if (rangedProjectilePrefab == null || rangedAttackPoint == null || playerCamera == null)
            {
                Debug.LogWarning("Projectile prefab, fire point, or camera is not assigned!");
                return;
            }

            // Instantiate and fire a ranged projectile
            GameObject projectile = Instantiate(rangedProjectilePrefab, rangedAttackPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Calculate the direction based on the camera's forward vector
                Vector3 cameraForward = playerCamera.transform.forward;
                Vector3 cameraPosition = playerCamera.transform.position;

                // Raycast to find the target point
                Ray ray = new Ray(cameraPosition, cameraForward);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f)) // Adjust range as needed
                {
                    // Point the projectile toward the hit position
                    Vector3 aimDirection = (hit.point - rangedAttackPoint.position).normalized;
                    rb.velocity = aimDirection * rangedAttackForce;
                }
                else
                {
                    // If nothing is hit, shoot in the camera's forward direction
                    rb.velocity = cameraForward * rangedAttackForce;
                }
            }

            // Assign damage to the projectile
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDamage(rangedDamage);
            }

        }
        else return;
        
        
    }

    public void ReloadWeapon()
    {
        bulletCnt = pistolMag;
    }

    public void EmptyMagSound()
    {
        emptyMagSound.Play();
    }

    public void ReloadMagSound()
    {
        reloadMagSound.Play();
    }

    // Visualize melee range in the editor
    private void OnDrawGizmosSelected()
    {
        if (meleeAttackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeRange);
    }
}