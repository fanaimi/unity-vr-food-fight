using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    
    // sphere to change color to
    [SerializeField] GameObject colorTarget;
    
    private MeshRenderer targetRend;
    private Color targetDefaultColor;
    private Color targetNewColor;
    
    private GrabbableObject hoveredObject;
    private GrabbableObject grabbedObject;

    public string grabButton;

    // Start is called before the first frame update
    void Start()
    {
        targetRend = colorTarget.GetComponent<MeshRenderer>();
        targetDefaultColor = targetRend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(grabButton))
        {
            // Grab
            if(hoveredObject != null)
            {
                grabbedObject = hoveredObject;
                hoveredObject = null;
                grabbedObject.OnGrabStart(this);
                targetRend.material.color = grabbedObject.hoverColor;
            }
        }

        if(Input.GetButtonUp(grabButton))
        {
            // Release
            if(grabbedObject != null)
            {
                targetRend.material.color = targetDefaultColor;
                grabbedObject.OnGrabEnd();
                grabbedObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check whether it tiggeres with an object that we can grab
        GrabbableObject tempObject = other.GetComponent<GrabbableObject>();

        if (tempObject != null)
        {
            hoveredObject = tempObject;
            hoveredObject.OnHoverEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check whether hand is getting away from an object that we can grab
        GrabbableObject tempObject = other.GetComponent<GrabbableObject>();

        if (tempObject != null && hoveredObject != null)
        {
            hoveredObject.OnHoverExit();
            hoveredObject = null;
        }
    }
}