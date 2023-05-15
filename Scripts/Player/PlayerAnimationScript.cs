using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    // Reference to main Player Script
    [SerializeField] internal PlayerScript playerScript;
    
    // Class references
    internal Animator animator;

    // Class variables
    private string IDLE = "Idle";
    private string WALK = "Walk";
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    internal void Animate()
    {
        if (playerScript.inputScript.movementInput != Vector2.zero)
        {
            animator.Play(WALK);
        }
        else
        {
            animator.Play(IDLE);
        }
    }
}
