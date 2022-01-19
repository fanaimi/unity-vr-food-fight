using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : FoodItem
{
   
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        print("grenade here");

        
    }
    
}
