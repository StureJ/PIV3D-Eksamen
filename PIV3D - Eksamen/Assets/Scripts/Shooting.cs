using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera FPScam;

    public float currentWeaponAmmo = 10f;
    public float overallWeaponAmmo = 50f;
    public float maxWeaponAmmo = 10f;

    public TMP_Text ammoCount;
    
    private float fireRate = 1f;

    private float timeSinceLastShot = 0f;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && (currentWeaponAmmo > 0))
        {
            Shoot();
            currentWeaponAmmo -= 1f;
            muzzleFlash.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        
        ammoCount.text = currentWeaponAmmo + "/" + overallWeaponAmmo;
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void Reload()
    {
        float ammoNeeded = maxWeaponAmmo - currentWeaponAmmo;

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
