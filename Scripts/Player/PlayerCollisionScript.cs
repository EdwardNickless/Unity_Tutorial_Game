using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    // Reference to main Player script
    [SerializeField] internal PlayerScript playerScript;

    public void DamagePlayer(int damageTaken)
    {
        playerScript.health -= damageTaken;

        // Player briefly flashes white to add a visual damage effect
        playerScript.playerArmScript.sr.material = playerScript.playerArmScript.whiteFlash;
        playerScript.playerBodyScript.sr.material = playerScript.playerBodyScript.whiteFlash;
        Invoke(nameof(ResetMaterial), 0.1f);

        if (playerScript.health <= 0)
        {
            playerScript.sceneScript.playerIsAlive = false;
            Destroy(playerScript.gameObject);
        }
    }

    // Resets the Player Material in Sprite Renderer to the default material
    private void ResetMaterial()
    {
        playerScript.playerArmScript.sr.material = playerScript.playerArmScript.defaultMat;
        playerScript.playerBodyScript.sr.material = playerScript.playerBodyScript.defaultMat;
    }


    private void ProcessCollision(GameObject collider)
    {
        if(collider.CompareTag("Enemy Projectile"))
        {
            // Sets the damage taken according to the projectile object
            DamagePlayer(collider.GetComponent<EnemyProjectileScript>().GetRangeDamage());
        }
        else if (collider.CompareTag("Enemy Melee"))
        {
            // Sets the damage taken according to the melee attack
            DamagePlayer(collider.GetComponentInParent<SkeletonCombatScript>().GetMeleeDamage());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        ProcessCollision(collider.gameObject);
    }
}
