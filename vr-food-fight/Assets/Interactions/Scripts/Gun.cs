using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Gun : GrabbableObject
{

    [SerializeField] private Bullet m_bulletPrefab;
    [SerializeField] private Transform m_munition;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        print("gun here");
        AudioManager.instance.Play("bang");
        m_munition.Rotate(25,0,0);
        
    }
    
}
