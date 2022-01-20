using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchButton : MonoBehaviour
{

    public Transform button;
    public Transform down;
    public AudioSource audioSource;

    private Vector3 originalPosition;

    public UnityEvent onButtonPressed;
    public UnityEvent onButtonReleased;
    
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = button.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.position = down.position;
            audioSource.Play();
            onButtonPressed?.Invoke();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.position = originalPosition;
            onButtonReleased?.Invoke();
        }
    }
}
