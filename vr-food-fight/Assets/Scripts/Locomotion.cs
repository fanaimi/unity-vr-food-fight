using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Locomotion : MonoBehaviour
{

    private XRHand controller;
    public Transform xrRig;

    private float playerSpeed = .8f;

    private LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRHand>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleRaycast();
    }

    /// <summary>
    /// Handling snap turns
    /// </summary>
    private void HandleRotation()
    {
        // projects settings > input manager > XRI_LEFT/RIGHT_Primary2DAxisClick
        // checking if thumbstick is pressed
        if (Input.GetButtonDown($"XRI_{controller.hand}_Primary2DAxisClick"))
        {
            // XRI_Left_Primary2D
            float rotation =
                Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Horizontal") > 0 ? 30 : -30;

            /*if (direction > 0)
            {
                // rotate right // angles in degrees
                xrRig.Rotate(0,30,0);
            }
            else
            {
                xrRig.Rotate(0,-30,0);
            }*/
            
            
            xrRig.Rotate(0, rotation, 0);

        }
    }



    /// <summary>
    /// this will handle smooth motion forward and sideways
    /// </summary>
    private void HandleMovement()
    {
        Vector3 forwardDirection = new Vector3(xrRig.forward.x,0 ,xrRig.forward.z);

        Vector3 rightDirection = new Vector3(xrRig.right.x, 0, xrRig.right.z);
        
        // getting values between 0 and 1
        forwardDirection.Normalize();
        rightDirection.Normalize();
        
        // getting axis directions
        float horizontal = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Horizontal");
        float vertical = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Vertical");
        

        // moving forward and backward
        xrRig.position += (vertical * Time.deltaTime * -forwardDirection * playerSpeed);

        // left and right 
        xrRig.position += (horizontal * Time.deltaTime * rightDirection * playerSpeed);
    } // HandleMovement


    /// <summary>
    /// handling raycast for teleportation
    /// </summary>
    void HandleRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit  hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hitInfo.point);
        }
        else
        {
            line.enabled = false;
        }

    } // HandleRaycast
}
