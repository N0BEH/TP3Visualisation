using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CharacterController controller;


    public float speed = 12f;

    //Script qui permet le déplacement de la caméra.
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);   
        
        if (Input.GetButton("Jump"))
        {
            Vector3 up = transform.up * 0.2f;
            controller.Move(up);
        }

        if (Input.GetButton("Scroutch"))
        {
            Vector3 down = transform.up * -0.2f;
            controller.Move(down);
        }
    }
}
