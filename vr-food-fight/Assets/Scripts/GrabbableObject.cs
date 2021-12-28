using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Color hoverColor;

    private MeshRenderer rend;
    private Color defaultColor;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        defaultColor = rend.material.color;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// this will change the color of the grabbable
    /// </summary>
    public void OnHoverEnter()
    {
        rend.material.color = hoverColor;
    }

    /// <summary>
    /// this will get back to normal color
    /// </summary>
    public void OnHoverExit()
    {
        rend.material.color = defaultColor;
    }

    public void OnGrabStart(XRHand hand)
    {
        transform.SetParent(hand.transform);
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void OnGrabEnd()
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}