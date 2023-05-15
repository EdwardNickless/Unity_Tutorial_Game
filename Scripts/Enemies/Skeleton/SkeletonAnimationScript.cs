using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationScript : MonoBehaviour
{
    // Reference to main Skeleton Controller
    [SerializeField] SkeletonScript skeletonScript;

    // General animation fields
    internal Animator animator;
    private string currentState;
    internal float length;

    // Looping Animation state constants
    private readonly string IDLE = "Idle";
    private readonly string WALK = "Walk";

    // Non-looping Animation state constants
    private readonly string ATTACKONE = "Attack One";
    private readonly string ATTACKTWO = "Attack Two";
    private readonly string BLOCK = "Block";
    private readonly string THROW = "Throw";

    // Conditional variables
    internal bool isBlocking;
    internal bool isThrowing;
    internal bool isInCombat;
    internal bool attackOne;
    internal bool attackTwo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isInCombat = false;
    }


    internal void Animate()
    {
        if(attackOne || attackTwo || isBlocking || isThrowing)
        {
            isInCombat = true;
        }
        
        // Idle and Walking animation
        if (!isInCombat)
        {
            if (!skeletonScript.movementScript.isChasing)
            {
                ChangeAnimationState(IDLE);
            }
            else
            {
                ChangeAnimationState(WALK);
            }
        }

        //-----------------------------------//
        //---------Combat animations---------//
        //-----------------------------------//
        
        // Close range attackOne animation
        if (attackOne)
        {
            ChangeAnimationState(ATTACKONE);
            Invoke(nameof(AttackOneComplete), length);
        }
        
        // Close range attackTwo animation
        if (attackTwo)
        {
            ChangeAnimationState(ATTACKTWO);
            Invoke(nameof(AttackTwoComplete), length);
        }

        // Block player attack animation
        if(isBlocking)
        {
            ChangeAnimationState(BLOCK);
            Invoke(nameof(BlockComplete), length);
        }

        // Throw Skeleton Sword animation
        if(isThrowing)
        {
            ChangeAnimationState(THROW);
            Invoke(nameof(SwordThrowComplete), length);
        }
    }

    // Updates the current animation
    // Assigns SkeletonAnimationScript.length to the the animation time duration in seconds as a float
    // Only runs if the new animation is different from the current animation
    private void ChangeAnimationState(string newState)
    {
        // Prevents new animation overriding current if it is the same
        if (newState == currentState)
        {
            return;
        }
        else
        {
        // Initiates the new animation
        animator.Play(newState);
        length = animator.GetCurrentAnimatorStateInfo(0).length;

        // Reassign currentState to the new animation
        currentState = newState;
        }
    }

    //-----------------------------------//
    //-----Methods to end animations-----// 
    //-----------------------------------//
    private void AttackOneComplete()
    {
        attackOne = false;
        isInCombat = false;
    }
    
    private void AttackTwoComplete()
    {
        attackTwo = false;
        isInCombat = false;
    }
    
    private void BlockComplete()
    {
        isBlocking = false;
        isInCombat = false;
    }
    
    private void SwordThrowComplete()
    {
        isThrowing = false;
        isInCombat = false;
    }
}
