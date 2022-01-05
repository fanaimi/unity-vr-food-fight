using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject // inheriting from GrabbableObject
{

    [SerializeField] private Rigidbody body;
    [SerializeField] private float throwBoost = 400f;

    private XRHand tempHand;

    public override void OnGrabStart(XRHand hand)
    {
        base.OnGrabStart(hand);
        // modification: storing hand reference
        tempHand = hand;
    }


    // OVERRIDE: overriding inherited OnGrabEnd (virtual)
    public override void OnGrabEnd()
    {
        // execute the inherited method content
        base.OnGrabEnd();
        
        // overriding section 
        Debug.Log("throw");
        body.AddForce(throwBoost * tempHand.transform.forward); // amount * direction
    } // OnGrabEnd
   
}
