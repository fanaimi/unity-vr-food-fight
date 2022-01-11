using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    
    // sphere to change color to
    [SerializeField] private GameObject colorTarget;
    // lerping speed
    // [SerializeField] [Range(0f, 1f)] 
    private float lerpTime;
    
    
    
    private MeshRenderer targetRend;
    private Color targetDefaultColor;
    private Color targetNewColor;

    private bool grabbing;
    
    private GrabbableObject hoveredObject;
    private GrabbableObject grabbedObject;

    public string grabButton;
    
    
    // session 9
    public Animator anim;
    
    // session 11
    public Hand hand = Hand.Left;
    
    
    // Start is called before the first frame update
    void Start()
    {
        lerpTime = .8f;
        targetRend = colorTarget.GetComponent<MeshRenderer>();
        targetDefaultColor = targetRend.material.color;

        grabButton = $"XRI_{hand}_GripButton";
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetButtonDown(grabButton))
        {
            // Grab
            if(hoveredObject != null  )
            {
                grabbing = true;
                grabbedObject = hoveredObject;
                hoveredObject = null;
                // if (grabbedObject.isColorProp)
                grabbedObject.OnGrabStart(this);
            } // hoveredObject
            
            // session 9 // accessing the Hand Animator
            anim.SetBool("Gripped", true);
        } // getButtonDown

        if (Input.GetButton(grabButton))
        {
            if (grabbing)
            {
                // Debug.Log("grabbing");
                if (grabbedObject.isColorProp)
                {
                    // transitioning colour
                    targetRend.material.color =
                        Color.Lerp(
                            targetRend.material.color,
                            grabbedObject.hoverColor,
                            lerpTime * Time.deltaTime);
                } 
            } // grabbing
        } // Input.GetButton 



        if(Input.GetButtonUp(grabButton))
        {
            // Release
            if(grabbedObject != null)
            {
                grabbing = false;
                // if (grabbedObject.isColorProp)
                if (true)
                {
                   targetRend.material.color = targetDefaultColor;
                   grabbedObject.OnGrabEnd();
                   grabbedObject = null; 
                   
                }
            }
                   
            // session 9 // accessing the Hand Animator
            anim.SetBool("Gripped", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check whether it triggers with an object that we can grab
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


[System.Serializable]
public enum Hand
{
    Left, 
    Right
}