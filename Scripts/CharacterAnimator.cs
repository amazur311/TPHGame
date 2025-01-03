using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private bool isWalking;
    public PlayerAttack playerAttack;
    //public int pistolMagCnt;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleMeleeCombat();
        HandlePistolCombat();
        HandleLongGunCombat();
    }

    private void HandleMovement()
    {
        // Check for movement input (WASD keys)
        bool movementKeyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (movementKeyPressed && !isWalking)
        {
            animator.SetBool("WalkStart", true);
            isWalking = true;
        }
        else if (!movementKeyPressed && isWalking)
        {
            animator.SetBool("WalkStart", false);
            isWalking = false;
        }
    }

    private void HandleMeleeCombat()
    {
        // Check if a melee weapon is equipped
        if (animator.GetBool("MeleeEquipped"))
        {
            // Ready melee weapon when right mouse button is held
            bool isMeleeReady = Input.GetMouseButton(1);
            animator.SetBool("MeleeReady", isMeleeReady);

            // If melee weapon is ready, handle backswing and foreswing
            if (isMeleeReady)
            {
                // Weapon backswing when left mouse button is held down
                if (Input.GetMouseButton(0))
                {
                    animator.SetBool("WeaponBackSwing-MouseDown", true);
                }

                // Weapon foreswing when left mouse button is released
                if (Input.GetMouseButtonUp(0))
                {
                    animator.SetBool("WeaponBackSwing-MouseDown", false);
                    animator.SetBool("WeaponForeSwing-MouseUp", true);
                }
            }
            else
            {
                // Reset backswing and foreswing if not in melee ready state
                animator.SetBool("WeaponBackSwing-MouseDown", false);
                animator.SetBool("WeaponForeSwing-MouseUp", false);
            }
        }
    }

    private void HandlePistolCombat()
    {
        // Check if a pistol is equipped
        if (animator.GetBool("PistolEquipped"))
        {
            // Ready pistol when right mouse button is held
            bool isPistolReady = Input.GetMouseButton(1);
            animator.SetBool("PistolReady", isPistolReady);
            bool reloadPistol = Input.GetKeyDown(KeyCode.R);

            if (reloadPistol)
            {
                ReloadPistol();
            }

            //if bulletMag is not empty, run animation, if empty 
            if (playerAttack.bulletCnt > 0)
            {
                // Pistol shooting when left mouse button is pressed
                if (isPistolReady && Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("PistolShoot-MouseDown", true);
                }
                else
                {
                    animator.SetBool("PistolShoot-MouseDown", false);
                }
            }
            else if (isPistolReady && Input.GetMouseButtonDown(0))
            {
                playerAttack.EmptyMagSound();
            }
        }
    }

    private void HandleLongGunCombat()
    {
        // Check if a pistol is equipped
        if (animator.GetBool("LongGunEquipped"))
        {
            // Ready pistol when right mouse button is held
            bool isLongGunReady = Input.GetMouseButton(1);
            animator.SetBool("LongGunReady", isLongGunReady);

            // Pistol shooting when left mouse button is pressed
            if (isLongGunReady && Input.GetMouseButtonDown(0))
            {
                animator.SetBool("LongGunShoot-MouseDown", true);
            }
            else
            {
                animator.SetBool("LongGunShoot-MouseDown", false);
            }
        }
    }

    public void WeaponEquip()
    {
        animator.SetBool("MeleeEquipped", false);
        animator.SetBool("PistolEquipped", false);
        animator.SetBool("LongGunEquipped", false);

        if (GameObject.Find("Crowbar")!= null)
        {
            animator.SetBool("MeleeEquipped", true);
        } else if(GameObject.Find("Pistol")!= null)
        {
            
            animator.SetBool("PistolEquipped", true);
        } else if (GameObject.Find("shotgun1") != null || GameObject.Find("sniper2"))
        {
            animator.SetBool("LongGunEquipped", true);
        }
    }

    public void ReloadPistol()
    {
        animator.SetTrigger("PistolReload");
       
    }


}
