using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : FoodItem
{

    private float m_delay = 3f;
    private float m_countdown;
    private bool m_isActive = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        m_countdown = m_delay;

    }

    private void Update()
    {
        if (m_isActive)
        {
            m_countdown -= Time.deltaTime;
            print(m_countdown);
        }

        if (m_countdown <= 0f)
        {
            Explode();
        }

    } // update


    private void Explode()
    {
        AudioManager.instance.Play("Grenade");
        Debug.Log("Boom");
    } // explode


    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        print("grenade here");

        
        
    } // oninteractionstart
    
    
    
    
    
}
