using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    // Reference to main player Script
    [SerializeField] internal PlayerScript playerScript;
    
    internal Vector2 movementInput;
    [SerializeField] Transform weaponsArm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Checks for input (W,A,S,D or Up,Left,Down,Right arrows)
    internal void PlayerMovementInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        //Normalizes the speed of the character when walking in a direction along the X and Y axes
        movementInput.Normalize();
    }

    internal void PlayerAimingInput()
    {
        // Rotates the current weapon around the characters hand using the mouse position.

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = playerScript.mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        weaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponsArm.localScale = Vector3.one;
        }
    }
}
