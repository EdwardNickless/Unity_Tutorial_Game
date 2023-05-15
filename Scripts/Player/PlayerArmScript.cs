using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmScript : MonoBehaviour
{
    // Reference to main Player script
    [SerializeField] PlayerScript playerScript;


    // Class references
    internal SpriteRenderer sr;


    // Class variables
    internal Material defaultMat;
    internal Material whiteFlash;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        defaultMat = sr.material;
        whiteFlash = Resources.Load("Materials/White Flash", typeof(Material)) as Material;
    }
}
