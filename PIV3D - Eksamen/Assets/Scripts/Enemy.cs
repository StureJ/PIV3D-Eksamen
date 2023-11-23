using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public Transform player;
    public float speed = 10f;
    public float stoppingDistance = 1f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
        if (Vector3.Distance(transform.position, player.position) < stoppingDistance)
        {
            Debug.Log("Player reached!");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
