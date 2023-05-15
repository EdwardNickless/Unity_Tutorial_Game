using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonCollisionScript : MonoBehaviour
{
    // Reference to main Skeleton Controller
    [SerializeField] SkeletonScript skeletonScript;

    
    // Object references
    [SerializeField] GameObject projectileBlockEffect;
    [SerializeField] GameObject[] damageEffects;


    // Class references
    private bool isBlocking;


    // Returns whether the Skeleton is blocking or not
    private bool IsBlocking() => isBlocking = skeletonScript.animationScript.isBlocking;

    // Resets the Skeletons Material in the Sprite Renderer to the default material
    internal void ResetMaterial()
    {
        skeletonScript.skeletonSR.material = skeletonScript.defaultMat;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }


    internal void ProcessCollision(GameObject collider)
    {
        if (collider.CompareTag("Player Bullet"))
        {
            int chance = Random.Range(0,101);

            if (IsBlocking() ||
                chance > 99)      // Skeleton blocks player projectile
            {
                skeletonScript.animationScript.isBlocking = true;

                Instantiate(projectileBlockEffect.transform,
                            collider.transform.position,
                            collider.transform.rotation);
            }

            else
            {
                int randomEffect = Random.Range(0, 5);

                Instantiate(damageEffects[randomEffect],
                            collider.transform.position,
                            collider.transform.rotation);

                // Skeleton briefly flashes white to add a visual damage effect
                skeletonScript.skeletonSR.material = skeletonScript.whiteFlash;
                Invoke(nameof(ResetMaterial), 0.1f);

                skeletonScript.combatScript.TakeDamage(skeletonScript.player.playerWeapon.damage);
            }
        }
    }
}
