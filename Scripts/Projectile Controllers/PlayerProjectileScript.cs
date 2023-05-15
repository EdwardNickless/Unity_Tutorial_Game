using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float bulletSpeed = 5f;
    
    [Tooltip("The effect generated when the bullet hits a wall")]
    [SerializeField] GameObject projectileWallEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2d.velocity = transform.right * bulletSpeed;
    }
    
    private void ProcessCollision(GameObject collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            Instantiate(projectileWallEffect.transform, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }
}
