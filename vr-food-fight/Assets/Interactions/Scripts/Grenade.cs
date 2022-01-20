using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : FoodItem
{

    private float m_delay = 3f;
    private float m_countdown;
    private bool m_isActive = false;
    private bool m_hasExploded = false;

    [SerializeField] private GameObject m_explosionParticles;
    [SerializeField] private float m_eplosionRadius = 20f;
    [SerializeField] private float m_explosionForce = 5000f;
    [SerializeField] private Zombie m_zombie;

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
            /*if (m_countdown > 0)
            {
                print(m_countdown);
            }*/
        }

        if (m_countdown <= 0f && !m_hasExploded)
        {
            Explode();
            m_hasExploded = true;
        }

    } // update


    private void Explode()
    {
        AudioManager.instance.Play("Grenade");
        // Debug.Log("Boom");
        
        // explosion effect ? particles ? bigExplosionEffect
        Instantiate(m_explosionParticles, transform.position, transform.rotation);
        
        // getting close objects
        Collider[] m_closeObjects = Physics.OverlapSphere(transform.position, m_eplosionRadius);

        foreach (Collider m_obj in m_closeObjects)
        {
            // adding exploding force
            Rigidbody m_rb = m_obj.GetComponent<Rigidbody>();
            // var m_brWin = m_obj.gameObject.GetComponent<BreakableWindow>();
            
           
                if (m_obj.gameObject.layer == 6) // Destructible
                { 
                    if (m_rb != null)
                    {
                        m_rb.isKinematic = false;
                        m_rb.useGravity = true; 
                        m_rb.AddExplosionForce(m_explosionForce, transform.position, m_eplosionRadius);
                    } // m_rb
                    
                    var m_brWin = m_obj.gameObject.GetComponent<BreakableWindow>();
                    if (m_brWin != null)
                    {
                        // Debug.Log("SDENG");
                        m_brWin.breakWindow();
                        Destroy(m_brWin.gameObject);
                    }
                }// destructible
        } // foreach
        m_zombie.Explode();
        // destroying grenade object
        Destroy(gameObject);
    } // explode


    public override void OnInteractionStart()
    {
        m_isActive = true;
        base.OnInteractionStart();
        print("grenade here");

        
        
    } // oninteractionstart
    
    
    
    
    
}
