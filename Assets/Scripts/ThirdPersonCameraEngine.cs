using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraEngine : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    void Start()
    {
        //mouse cursors disappear when starting
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        //prevence the camera from flip around and getting to high
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        //rotate only the camera
        if (Input.GetKey(KeyCode.LeftAlt))
        {

            //quaternions are how unity handles rotations
            //Euler are the rotation degrees
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            //rotate the camera and player
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }


}
