using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : GrabbableObject
{

    [SerializeField] private GameObject m_paintTrailPrefab;
    [SerializeField] private Transform m_paintbrushTip;

    private GameObject m_spawnedPaint;
    private TrailRenderer m_trailRend;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    
    
    
    
    
    
    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        //print("brush here");

        m_spawnedPaint = Instantiate(m_paintTrailPrefab, m_paintbrushTip.position, m_paintbrushTip.rotation);
        m_trailRend = m_paintTrailPrefab.GetComponent<TrailRenderer>();
    } // oninteractionstart


    public override void OnInteractionUpdating()
    {
        // base.OnInteractionUpdating();
        if (m_spawnedPaint)
        {
            m_spawnedPaint.transform.position = m_paintbrushTip.position;
        }


    } // updating

    public override void OnInteractionStop()
    {
        base.OnInteractionStop();

        m_spawnedPaint.transform.position = m_spawnedPaint.transform.position;
    } // stop
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (m_trailRend != null && other.CompareTag("RedPaint"))
        {
            print("red");
            // m_trailRend.colorGradient.mode = GradientMode.Fixed;
            m_trailRend.GetComponent<TrailRenderer>().startColor = Color.red;
            m_trailRend.GetComponent<TrailRenderer>().endColor = Color.white;
        }
        
        
        if (m_trailRend != null && other.CompareTag("GreenPaint"))
        {
            print("green");
            // m_trailRend.colorGradient.mode = GradientMode.Fixed;
            m_trailRend.GetComponent<TrailRenderer>().startColor = Color.green;
            m_trailRend.GetComponent<TrailRenderer>().endColor = Color.white;
        }
    }
}
