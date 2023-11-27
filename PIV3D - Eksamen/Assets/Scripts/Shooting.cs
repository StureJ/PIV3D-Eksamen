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

    private bool isHeavyWeapon = false;
    public GameObject assaultWeapon, heavyWeapon;

    private float fireRate = 1f;

    private float timeSinceLastShot = 0f;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && (currentWeaponAmmo > 0) && !isHeavyWeapon)
        {
            Shoot();
            currentWeaponAmmo -= 1f;
        }

        if (Input.GetButton("Fire1") && (currentWeaponAmmo > 0) && isHeavyWeapon)
        {
            if (timeSinceLastShot >= 1f / fireRate)
            {
                Shoot();
                currentWeaponAmmo -= 1f;
                timeSinceLastShot = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWeapon();
        }

        ammoCount.text = currentWeaponAmmo + "/" + overallWeaponAmmo;
        Debug.Log(currentWeaponAmmo);
        Debug.Log(overallWeaponAmmo);
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

    void ToggleWeapon()
    {
        isHeavyWeapon = !isHeavyWeapon;

        if (isHeavyWeapon)
        {
            HeavyGun();
            assaultWeapon.SetActive(false);
            heavyWeapon.SetActive(true);
        }
        else
        {
            damage = 10f;
            range = 100f;
            currentWeaponAmmo = 10f;
            overallWeaponAmmo = 50f;
            maxWeaponAmmo = 10f;

            assaultWeapon.SetActive(true);
            heavyWeapon.SetActive(false);
        }
    }

    void HeavyGun()
    {
        damage = 5f;
        range = 100f;
        currentWeaponAmmo = 50f;
        overallWeaponAmmo = 150f;
        maxWeaponAmmo = 50f;
        fireRate = 10f;
    }
}
