 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;
 using UnityEngine.SceneManagement;

 public class playerMovement : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerShield = 50f;
    
    public CharacterController controller;
    public float speed = 12f;

    public TMP_Text HealthText, ShieldText,KillCount;
    public float KillCounter = 0f;
    
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));

        HealthText.text = playerHealth.ToString();
        ShieldText.text = playerShield.ToString();
        KillCount.text = "Enemies slain: " + KillCounter;

        if (playerHealth <= 0 & playerShield <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Level1");
    }
}
