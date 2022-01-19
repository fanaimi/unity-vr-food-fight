using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GrabbableObject
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        print("gun here");
    }
    
}
