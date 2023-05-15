using System;
using System.Collections;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class SkeletonScript : MonoBehaviour
{
    // Reference to Scene Script
    private SceneScript sceneScript;
    

    // References to supporting Scipts
    [SerializeField] internal SkeletonMovementScript movementScript;
    [SerializeField] internal SkeletonCombatScript combatScript;
    [SerializeField] internal SkeletonCollisionScript collisionScript;
    [SerializeField] internal SkeletonAnimationScript animationScript;


    // Reference to the player
    internal PlayerScript player;


    // Class references
    private GameObject deathEffect;
    internal Rigidbody2D rb2d;
    internal SpriteRenderer skeletonSR;
    private SpriteRenderer[] sr;


    // Class variables
    internal Material defaultMat;
    internal Material whiteFlash;


    // Skeletons target
    internal Transform playerTransform; 
    

    //Start is called before the first frame update
    void Start()
    {
        // Initialise references
        sceneScript = FindObjectOfType<SceneScript>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        skeletonSR = GetComponent<SpriteRenderer>();
        
        player = FindObjectOfType<PlayerScript>();
        playerTransform = player.GetComponent<Transform>().transform;
        deathEffect = Resources.Load("Prefabs/Enemies/Skeleton/Death", typeof(GameObject)) as GameObject;

        // Initialise class variables
        defaultMat = skeletonSR.material;
        whiteFlash = Resources.Load("Materials/White Flash", typeof(Material)) as Material;
    }


    // Returns the current state of the skeleton in the scene
    internal void GetStatus()
    {
        if (player)
        {
            if (animationScript.attackOne || animationScript.attackTwo ||
                animationScript.isBlocking || animationScript.isThrowing)
            {
                animationScript.isInCombat = true;
            }
            movementScript.FaceInDirection();
            movementScript.ChasePlayer();
            animationScript.Animate();
            combatScript.AttackPlayer();
            combatScript.ShootPlayer();
        }
    }


    // Skeleton death
    internal void SkeletonDies()
    {
        // gameObject.SetActive(false); // Hide Skeleton from view
        foreach (SpriteRenderer sRenderer in sr)
        {
            sRenderer.forceRenderingOff = true;
        }

        Instantiate(deathEffect, transform.position, transform.rotation); // Show the Skeleton death effect

        sceneScript.skeletonIsAlive = false;
        Destroy(gameObject);
    }


    // Draw the different Skeleton ranges in the Scene view, if Skeleton is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, movementScript.combatRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movementScript.chaseRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, movementScript.keepChaseRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, combatScript.shootingRange);
    }
}
