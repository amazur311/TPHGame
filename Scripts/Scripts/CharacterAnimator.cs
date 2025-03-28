using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private bool isWalking;
    public PlayerAttack playerAttack;
    public Health health;
    
    public event Action<int> OnEscapeGrapple;
    public int grappleCnt;
    
    //public int pistolMagCnt;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        AnimationEvents.OnAnyEnemyGrapple += GrappleStart;
    }

    private void OnDestroy()
    {
        AnimationEvents.OnAnyEnemyGrapple -= GrappleStart;
    }

    void Update()
    {
        HandleMovement();
        HandleMeleeCombat();
        HandlePistolCombat();
        HandleLongGunCombat();
        
        if(animator.GetBool("Grappled") == true)
        {
            grappleCnt = HandleGrapple(grappleCnt) ;
        }

        health.OnTakeDamage += HandleTakeDamage;
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

            if (reloadPistol && playerAttack.pistolTotal>0)
            {
                ReloadPistolAnim();
                
            }

            //if bulletMag is not empty, run animation, if empty 
            if (playerAttack.bulletCntPistol > 0)
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
            bool reloadLongGun = Input.GetKeyDown(KeyCode.R);

            if (reloadLongGun && playerAttack.shotgunTotal > 0)
            {
                ReloadShotgunAnim();

            }

            if (playerAttack.bulletCntShotgun > 0)
            {
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
            else if (isLongGunReady && Input.GetMouseButtonDown(0))
            {
                playerAttack.EmptyMagSound();
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

    public void ReloadPistolAnim()
    {
        animator.SetTrigger("PistolReload");
       
    }

    public void ReloadShotgunAnim()
    {
        animator.SetTrigger("ShotgunReload");
    }

    void HandleTakeDamage(float damage)
    {
        if (animator != null)
        {
        if (!animator.GetBool("Grappled"))
            {
                animator.SetTrigger("TakeDamage");
            }
                
            
        }
    }

    public void GrappleStart(bool grappleMade)
    {
        animator.SetBool("Grappled", grappleMade);
    }

    public int HandleGrapple(int escGrappleCnt)
    {
        if ( escGrappleCnt<9)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                escGrappleCnt++;
            }
        }
        else
        {
            animator.SetBool("Grappled", false);
            escGrappleCnt = 0;
        }

        return (escGrappleCnt);

    }


}
