 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;
 using UnityEngine.SceneManagement;

 public class playerMovement : MonoBehaviour
{
    //Variables for health & shield
    public float playerHealth = 100f;
    public float playerShield = 50f;
    
    public CharacterController controller;
    public float speed = 12f;

    public TMP_Text HealthText, ShieldText,KillCount;
    public float KillCounter = 0f;
    
    // Update is called once per frame
    void Update()
    {
        //Gets the input from the horizontal and vertical axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Calculates the direction the player has to move
        Vector3 move = transform.right * x + transform.forward * z;

        //Calculates the speed and moves the character
        controller.Move(move * (speed * Time.deltaTime));

        //Updates the UI
        HealthText.text = playerHealth.ToString();
        ShieldText.text = playerShield.ToString();
        KillCount.text = "Enemies slain: " + KillCounter;

        //If the player has 0 health & shield, it calls the Die() function
        if (playerHealth <= 0 & playerShield <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //The scene is reloaded, aka the level resets.
        SceneManager.LoadScene("Level1");
    }
}
