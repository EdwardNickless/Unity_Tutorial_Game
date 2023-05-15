using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Reference to Scene Script
    internal SceneScript sceneScript;
    
    
    // References to other Player Scripts
    [SerializeField] internal PlayerInputScript inputScript;
    [SerializeField] internal PlayerMovementScript movementScript;
    [SerializeField] internal PlayerCombatScript combatScript;
    [SerializeField] internal PlayerCollisionScript collisionScript;
    [SerializeField] internal PlayerAnimationScript animationScript;
    [SerializeField] internal PlayerBodyScript playerBodyScript;
    [SerializeField] internal PlayerArmScript playerArmScript;
    [SerializeField] internal PlayerWeapon playerWeapon;


    // Class references
    internal Camera mainCamera;
    internal Rigidbody2D rb2d;


    // Class variables
    [SerializeField] internal int health = 10;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        // Initialise references
        sceneScript = FindObjectOfType<SceneScript>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    // Returns the current state of the player in the scene
    internal void GetStatus()
    {
        inputScript.PlayerMovementInput();
        inputScript.PlayerAimingInput();
        movementScript.MovementSpeed();
        playerWeapon.SetDamage();
        combatScript.Shoot();
        animationScript.Animate();
    }
}
