using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector3 targetPosition; 


    void Awake()
    {
        targetPosition = new Vector3(-5.42f, 0.248f, 1.457f);
    }
}
