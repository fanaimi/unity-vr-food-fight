using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : GrabbableObject
{

    [SerializeField] private GameObject m_paintTrailPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    
    
    
    
    
    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        print("brush here");

        
        
    } // oninteractionstart


    public override void OnInteractionUpdating()
    {
        base.OnInteractionUpdating();
    } // updating

    public override void OnInteractionStop()
    {
        base.OnInteractionStop();
    } // stop
}
