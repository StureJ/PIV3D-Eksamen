using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //damage for the gun
    public float damage = 10f;
    //Range for the gun
    public float range = 100f;

    public Camera FPScam;

    //Ammunition counter
    public float currentWeaponAmmo = 10f;
    public float overallWeaponAmmo = 50f;
    public float maxWeaponAmmo = 10f;

    //Text for displaying the ammunition 
    public TMP_Text ammoCount;
    
    private float fireRate = 1f;
    
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {

        //If-statement for detecting that the player has the ammo required to shoot, detects input for mouse1
        if (Input.GetButtonDown("Fire1") && (currentWeaponAmmo > 0))
        {
            //Calls the shoot function
            Shoot();
            //Subtracts 1 ammunition per input
            currentWeaponAmmo -= 1f;
            //plays the participle system
            muzzleFlash.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        
        //Updates the text on the Canvas
        ammoCount.text = currentWeaponAmmo + "/" + overallWeaponAmmo;
    }

    void Shoot()
    {
        RaycastHit hit;
        //Sends out a raycast forward from the Cameras position with a range of 100. 
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            //Gets access to the script attached to the enemy
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                //Calls the function TakeDamage() from the enemy's script
                enemy.TakeDamage(damage);
            }
        }
    }

    void Reload()
    {
        //Calculates the ammunition needed
        float ammoNeeded = maxWeaponAmmo - currentWeaponAmmo;

        //If there is more or equal ammunition available, then it gets the ammo in the gun to 10, and subtracts the ammoneeded
        //from the overallweaponammo variable.
        if (overallWeaponAmmo >= ammoNeeded)
        {
            currentWeaponAmmo = maxWeaponAmmo;
            overallWeaponAmmo -= ammoNeeded;
        }
        else
        {
            currentWeaponAmmo += overallWeaponAmmo;
            overallWeaponAmmo = 0;
        }
    }
}
