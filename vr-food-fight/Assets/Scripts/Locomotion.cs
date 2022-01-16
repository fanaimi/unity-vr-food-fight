using System;
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
    
    // session 12
    public Vector3 hitPosition;

    private Vector3 rigOriginalRotation;

    private void Awake()
    {
        rigOriginalRotation = xrRig.rotation.ToEulerAngles();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRHand>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.positionCount = lineResolution + 1;
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
    /// Line Renderer
    /// </summary>
    void HandleRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit  hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            line.enabled = true;
            /* straight line
             line.SetPosition(0, transform.position);
            line.SetPosition(1, hitInfo.point);*/
            hitPosition = hitInfo.point;
            CurveLine(hitInfo.point);


            bool validTarget = hitInfo.collider.CompareTag("teleportation");
            bool portalTarget = hitInfo.collider.CompareTag("Portal");
            Color color;

            if (validTarget)
            {
                color = Color.blue;
            } else if (portalTarget)
            {
                color = Color.green;
            }
            else
            {
                color = Color.red;
            }


            line.endColor = color;
            line.startColor = color;

            if (Input.GetButtonDown($"XRI_{controller.hand}_TriggerButton"))
            {
                // snap movement
                // xrRig.position = hitInfo.point;
                // fading and teleporting

                if (validTarget)
                {
                    StartCoroutine(FadeTeleport(
                        hitPosition, 
                         //Quaternion.identity.ToEulerAngles()
                        //Vector3.forward
                        xrRig.rotation.ToEulerAngles()
                        )
                    );
                } else if (portalTarget)
                {
                    // Debug.Log(hitInfo.collider.GetComponent<Portal>().targetPosition);
                    StartCoroutine(FadeTeleport(
                            hitInfo.collider.GetComponent<Portal>().targetPosition,
                            rigOriginalRotation
                        )
                    );
                }

            }// trigger button down
            

        }
        else
        {
            line.enabled = false;
        }

    } // HandleRaycast


    //              B
    //    A                     C
    
    // session 12
    public float height = 2f;
    [Range(5,40)] // number of indexes in the curved line
    public int lineResolution = 10;
    void CurveLine(Vector3 hitPosition)
    {
        Vector3 A = controller.transform.position;
        Vector3 C = hitPosition;
        Vector3 B = (C - A) / 2 + A;

        B.y += height;

        for (int i = 0; i <= lineResolution ; i++)
        {
            float t = (float)i / (float)lineResolution;
            Vector3 AtoB = Vector3.Lerp(A, B, t);
            Vector3 BtoC = Vector3.Lerp(B, C, t);
            Vector3 curvePosition = Vector3.Lerp(AtoB, BtoC, t);

            
            line.SetPosition(i, curvePosition);
            
        }
    }

    public Renderer screen;

    private IEnumerator FadeTeleport(Vector3 teleportTarget, Vector3 targetDir )
    {
        float currentTime = 0f;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            screen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }

        // xrRig.position = hitPosition;
        xrRig.position = teleportTarget;
        
        
        /*
        xrRig.rotation = Quaternion.Euler(
            rotationAtTarget
        );*/

        /*xrRig.rotation.y = rotationAtTarget.y;*/
        
        

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = rotation;
        
        // Rotate needs an angle
        // xrRig.Rotate(0f, 90f, 0f); 
        // xrRig.Rotate(rotationAtTarget);

        yield return new WaitForSeconds(.5f);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            screen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }
    }

}
