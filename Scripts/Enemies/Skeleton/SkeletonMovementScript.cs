using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovementScript : MonoBehaviour
{
    // Reference to main Skeleton Controller
    [SerializeField] SkeletonScript skeletonScript;

    // Variables
    private Vector3 directionToMoveIn;
    private float enemySpeed = 2f;
    internal float chaseRange = 5f;
    internal float keepChaseRange = 7f;
    internal float combatRange = 2f;
    internal bool isChasing;


    //Makes the skeleton face the player on the x axis
    internal void FaceInDirection()
    {
        if (skeletonScript.playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }


    // Set direction for the Skeleton to move in and normalize
    private void SetDirection()
    {
        directionToMoveIn = skeletonScript.playerTransform.position - transform.position;
        directionToMoveIn.Normalize();
    }

    private void StopMoving()
    {
        skeletonScript.rb2d.velocity = Vector3.zero;
    }
    
    
    // Makes the Skeleton chasethe player
    internal void ChasePlayer()
    {
        if (Vector3.Distance(skeletonScript.playerTransform.position, transform.position) <= combatRange)
        {
            isChasing = false;
            StopMoving();
        }
        else
        {
            if (Vector3.Distance(skeletonScript.playerTransform.position, transform.position) < chaseRange)

            {
                SetDirection();
                isChasing = true;
            }
            else if (Vector3.Distance(skeletonScript.playerTransform.position, transform.position) < keepChaseRange && isChasing)
            {
                SetDirection();
            }
            else
            {
                isChasing = false;
                StopMoving();
            }
        }
        
        // Only move if the Skeleton is not in Combat (Attacking, Blocking, Throwing Sword)
        if (skeletonScript.animationScript.isInCombat)
        {
            skeletonScript.rb2d.velocity = Vector3.zero;
        }
        else
        {
            skeletonScript.rb2d.velocity = directionToMoveIn * enemySpeed;
        }
    }
}
