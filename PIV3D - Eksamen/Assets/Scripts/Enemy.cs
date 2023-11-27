using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 20f;
    public Transform player;
    public float speed = 10f;
    public float stoppingDistance = 1f;

    private playerMovement playerHP;
    public bool canAttack = true;
    public float attackSpeed = 3f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.transform;

        // Assuming playerMovement script is attached to the player GameObject
        playerHP = playerObject.GetComponent<playerMovement>();
    }

    public void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, player.position) < stoppingDistance)
        {
            attackSpeed -= Time.deltaTime;
            if (attackSpeed <= 0)
            {
                attackSpeed = 3f;
                canAttack = true;
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (canAttack)
        {
            enemyAttack();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        playerHP.KillCounter += 1f;
    }

    void enemyAttack()
    {
        if (playerHP.playerShield > 0)
        {
            playerHP.playerShield -= 10f;
        }
        else
        {
            playerHP.playerHealth -= 10f;
        }
        canAttack = false;
    }
}