using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    
    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor when we start the game.
    }

   
    void Update()
    {
        //Get our X and Y axis for the mouse movement and set the sensitivity to delta time.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //Lock the rotation of the mouse so it doesn't go past a certain point.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        //transform the rotation (make it rotate).
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //Rotate with the players body on the X axis
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
