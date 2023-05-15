using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    private Vector3 playerDirection;
    [SerializeField] GameObject projectileImpactEffect;
    int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerDirection = FindObjectOfType<PlayerScript>().transform.position - transform.position;
        playerDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerDirection * projectileSpeed * Time.deltaTime;
    }


    // Returns the damage dealt by this projectile
    public int GetRangeDamage()
    {
        return damage;
    }


    private void ProcessCollision()
    {
        Instantiate(projectileImpactEffect.transform, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision();
    }
}
