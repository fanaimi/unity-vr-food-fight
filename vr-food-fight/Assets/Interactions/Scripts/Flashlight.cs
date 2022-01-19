using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : GrabbableObject
{


    private Light m_flashlight;
    // Start is called before the first frame update
    void Start()
    {
        m_flashlight = GetComponentInChildren<Light>();
        m_flashlight.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    public override void OnInteractionStart()
    {
        base.OnInteractionStart();
        print("flashlight here");
        m_flashlight.enabled = !m_flashlight.enabled;
    }
}
