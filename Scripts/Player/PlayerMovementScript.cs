using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Reference to main Player script
    [SerializeField] internal PlayerScript playerScript;
    
    internal int movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 6;
    }

    // Set the velocity of the RigidBody attached the the Player (Make the player move)

    internal void MovementSpeed()
    {
        playerScript.rb2d.velocity = movementSpeed * playerScript.inputScript.movementInput;
    }
}
