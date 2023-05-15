using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Reference to main Player script
    [SerializeField] PlayerScript playerScript;

    
    // Class variables
    internal int damage;


    // Sets the amount of damage that the player deals
    internal void SetDamage()
    {
        damage = 1;
    }
}
