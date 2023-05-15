using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonCombatScript : MonoBehaviour
{
    // Reference to main Skeleton Script
    [SerializeField] SkeletonScript skeletonScript;


    // Prefabs and child objects of Skeleton
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] GameObject projectileBlockEffect;
    [SerializeField] Transform firePosition;


    // Class variables
    internal int health;
    internal int meleeDamage;

    internal float throwRangeMin;
    internal float shootingRange;
    private float timeBetweenSwordThrows;

    private bool readyToSlash;
    private bool meleeAttacker;
    private bool readyToShoot;


    // Melee attacks
    [SerializeField] internal GameObject attackOne;
    private int attackOneDamage = 1;

    [SerializeField] internal GameObject attackTwo;
    private int attackTwoDamage = 3;

    // Initialise the Skeletons combat variables when spawned
    internal void InitialiseSkeleton()
    {
        health = 10;
        shootingRange = 10f;
        timeBetweenSwordThrows = 1f;
        throwRangeMin = skeletonScript.movementScript.chaseRange;
        readyToShoot = true;
        readyToSlash = true;
    }

    // Returns the damage for the current melee attack
    internal int GetMeleeDamage()
    {
        return meleeDamage;
    }
    
    // Sets the damage for the current melee attack
    private void SetMeleeDamage(int newDamage)
    {
        meleeDamage = newDamage;
    }

    // Skeleton takes damage
    internal void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            skeletonScript.SkeletonDies();
        }
    }


    // Checks if player is within striking range
    private void MeleeCheck()
    {
        if (Vector3.Distance(skeletonScript.playerTransform.position, transform.position) > throwRangeMin)
        {
            meleeAttacker = false;
        }
        else
        {
            meleeAttacker = true;
        }
    }

    // Melee attack
    internal void AttackPlayer()
    {
        MeleeCheck();
        if (meleeAttacker &&
            readyToSlash &&
            Vector3.Distance(skeletonScript.playerTransform.position, transform.position) < throwRangeMin - 1f)
        {
            readyToSlash = false;
            //Slash the Player
            int attackChoice = Random.Range(0, 100);
            if (attackChoice <= 66)
            {
                SetMeleeDamage(attackOneDamage);
                StartCoroutine(AttackOne()); //See IEnumerator AttackOne() below
            }
            else
            {
                SetMeleeDamage(attackTwoDamage);
                StartCoroutine(AttackTwo()); //See IEnumerator AttackTwo() below
            }
        }
    }


    // Long range attack
    internal void ShootPlayer()
    {
        MeleeCheck();
        if (!meleeAttacker &&
            readyToShoot &&
            Vector3.Distance(skeletonScript.playerTransform.position, transform.position) < shootingRange)
        {
            readyToShoot = false;
            //Shoot the projectile
            StartCoroutine(FireEnemyProjectile()); //See IEnumerator FireEnemyProjectile() below
        }
    }

    // IEnumerators for Coroutines

    IEnumerator FireEnemyProjectile() // IEnumerator for ShootPlayer()
    {
        skeletonScript.animationScript.isThrowing = true; // Instantiate sword throw animation (Skeleton sprite)
        yield return new WaitForSeconds(skeletonScript.animationScript.length - 0.2f);
        Instantiate(enemyProjectile, firePosition.position, firePosition.rotation); // Instantiate sword throw attack
        yield return new WaitForSeconds(timeBetweenSwordThrows);
        readyToShoot = true;
    }

    IEnumerator AttackOne() // IEnumerator for AttackOne()
    {
        yield return new WaitForSeconds(1f);
        skeletonScript.animationScript.attackOne = true; // Invoke Attack One animation
        yield return new WaitForSeconds(0.35f);
        attackOne.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackOne.SetActive(false);
        readyToSlash = true;
    }
        
    IEnumerator AttackTwo() // IEnumerator for AttackTwo()
    {
        yield return new WaitForSeconds(1f); 
        skeletonScript.animationScript.attackTwo = true; // Invoke Attack Two animation
        yield return new WaitForSeconds(0.35f);
        attackTwo.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackTwo.SetActive(false);
        readyToSlash = true;
    }
}
