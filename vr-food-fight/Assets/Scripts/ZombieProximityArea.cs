using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieProximityArea : MonoBehaviour
{

    [SerializeField] private Zombie m_zombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("out");
        }
    }
}
