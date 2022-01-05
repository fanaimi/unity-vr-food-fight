using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Color hoverColor;

    [SerializeField] public bool isColorProp;
    [SerializeField] public bool isFruit;
    
    private MeshRenderer rend;
    private Color defaultColor;
    private Rigidbody rb;

    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (isColorProp)
        {
            rend = GetComponent<MeshRenderer>();
            defaultColor = rend.material.color;
        }

            rb = GetComponent<Rigidbody>();
        
    }

   

    /// <summary>
    /// this will change the color of the grabbable
    /// </summary>
    public void OnHoverEnter()
    {
        if (isColorProp)
        {
            rend.material.color = hoverColor;
        }
    }

    /// <summary>
    /// this will get back to normal color
    /// </summary>
    public void OnHoverExit()
    {

        if (isColorProp)
        {
            rend.material.color = defaultColor;
        }
    }

    public virtual void OnGrabStart(XRHand hand)
    {
        transform.SetParent(hand.transform);
        rb.useGravity = false;
        rb.isKinematic = true;
        // read https://medium.com/@danielcestrella/an-improved-way-of-grabbing-objects-in-vr-with-unity3d-558517a8db1
    }

    // VIRTUAL can be overridden by an inheriting class (overriding)
    public virtual void OnGrabEnd()
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}