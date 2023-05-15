using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    // Reference to main Player script
    [SerializeField] PlayerScript playerScript;

    // References to Prefabs / Related components
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    // Class variables
    private bool readyToShoot;
    private float shotDelay;
    private float shotCounter;
    private float timeBetweenShots;
    
    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        shotDelay = 0.2f;
        timeBetweenShots = 1f;
    }

    // IEnumerators
    IEnumerator ShotTimer() // Countdown between shots (Semi-automatic Weapon)
    {
        yield return new WaitForSeconds(shotDelay);
        readyToShoot = true;
    }

    IEnumerator ShotTimerAutomatic() // Countdown between shots (Automatic Weapons)
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    internal void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && readyToShoot)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            readyToShoot = false;
            StartCoroutine(ShotTimer());
        }
    }
}
