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
    
    //Variable for updating the color / material of the enemy
    private SkinnedMeshRenderer skinnedMeshRenderer;

    public void TakeDamage(float amount)
    {
        //If the raycast hits the enemy, it takes damage
        health -= amount;
        if (health <= 0)
        {
            //If the enemy's health is equal or less then zero it calls the function die()
            Die();
        }
    }

    public void Start()
    {
        //Finds the player character & its transform
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.transform;

        //gains access to the playermovement script on the player & skinnedmeshrenderer on itself.
        playerHP = playerObject.GetComponent<playerMovement>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void Update()
    {
        //Calculates the direction towards the player
        Vector3 direction = player.position - transform.position;
        //The y is ignored because i had problems with it floating up and downwards
        direction.y = 0f;
        //The vector is normalized, so its "only" the direction towards the player thats relevant.
        direction.Normalize();
        
        //The enemy always face towards the player
        transform.LookAt(player);

        //the enemy is moved towards the player with a set speed
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        //Checks if the player is within range
        if (Vector3.Distance(transform.position, player.position) < stoppingDistance)
        {
            //A timer for attacking the player, it is counting down from 3.
            attackSpeed -= Time.deltaTime;
            if (attackSpeed <= 0)
            {
                //Timer is reset and the boolean value is set to true.
                attackSpeed = 3f;
                canAttack = true;
            }
        }

        //It changes the color of the prefab if its under or 10 health
        if (health <= 10)
        {
            skinnedMeshRenderer.material.color = Color.red;
        }
        
    }

    //If it collides with the player and the boolean value canAttack is set to true, it calls enemyAttack()
    private void OnTriggerStay(Collider other)
    {
        if (canAttack)
        {
            enemyAttack();
        }
    }

    void Die()
    {
        //the enemy is removed from the scene (it dies) and the killcounter and shield variable
        //on the player is updated and added.
        Destroy(gameObject);
        playerHP.KillCounter += 1f;
        playerHP.playerShield += 10f;
    }

    void enemyAttack()
    {
        //It first subtracts from the shield, if the shield is 0, then it subtracts from the health
        if (playerHP.playerShield > 0)
        {
            playerHP.playerShield -= 10f;
        }
        else
        {
            playerHP.playerHealth -= 10f;
        }
        //The enemy can't attack, it has to wait for the timer of 3 seconds.
        canAttack = false;
    }
}