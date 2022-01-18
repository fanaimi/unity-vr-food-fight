using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject // inheriting from GrabbableObject
{

    [SerializeField] private Rigidbody body;
     private float throwBoost = 800f;

    private XRHand tempHand;

    
    // session 9
    public enum GrabType
    {
        Kinematic,
        JointBased
    }
    [SerializeField] private GrabType grabbingType;
    private FixedJoint joint;

    private Vector3 previousPosition;

    private Queue<Vector3> previousVelocities = new Queue<Vector3>();
    [SerializeField] private int numOfVelocitySamples;
    private void FixedUpdate()
    {
        Vector3 velocity = transform.position - previousPosition;
        previousPosition = transform.position;
        
        // enqueue adds at the end (kinda like push)
        previousVelocities.Enqueue(velocity);
        
        if (previousVelocities.Count > numOfVelocitySamples)
        {
            // removing first value
            previousVelocities.Dequeue();
        }
    }


    public override void OnGrabStart(XRHand hand)
    {
        // Debug.Log("THROWABLE on grab START");
        // modification: storing hand reference
        tempHand = hand;
        if (grabbingType == GrabType.Kinematic)
        {
            base.OnGrabStart(hand); // kinematic - child parent connection
        } // kinematic
        
        else if (grabbingType == GrabType.JointBased)
        {
            //adding a new fixed joint component to grabbable
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = hand.GetComponent<Rigidbody>();
        } // joint 

    }


    // OVERRIDE: overriding inherited OnGrabEnd (virtual)
    public override void OnGrabEnd()
    {
        // Debug.Log("THROWABLE on grab end");
        if (grabbingType == GrabType.Kinematic)
        {
            // execute the inherited method content
            base.OnGrabEnd();
            body.AddForce(throwBoost * tempHand.transform.forward);
        } // kinematic
        
        else if (grabbingType == GrabType.JointBased)
        {
            // removing the joint 
            Destroy(joint);
                // } // joint
        
        
            // overriding section 
            // Debug.Log(isFruit);
            // shooting
            // body.AddForce(throwBoost * tempHand.transform.forward); // amount * direction
            /*if (isFruit)
            {
                Destroy(gameObject, 5);
            }*/
            
            Vector3 averageVelocity = Vector3.zero; // same as (0,0,0)

            foreach (var tempVelocity in previousVelocities)
            {
                averageVelocity += tempVelocity; // total of velocities
            }

            averageVelocity /= previousVelocities.Count; // average velocity

            body.velocity = averageVelocity * throwBoost;
        
        } // joint 
    } // OnGrabEnd



    


}
