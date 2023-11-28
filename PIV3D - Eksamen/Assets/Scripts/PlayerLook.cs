using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public Transform playerBody;

    private float xRotation = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //The cursor disappears from the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Two variables that gets the input from the x and y position of the mouse, times it by the sensivity (100)
        // and time.deltatime, because it has to be the same regardless of frames per second.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        //Makes the camera move up and down
        xRotation -= mouseY;
        
        //Prevents the user from turning the camera all the way (so its "more" realistic)
        xRotation = math.clamp(xRotation, -90f, 90f);
        
        //Makes the camera & player character move
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
