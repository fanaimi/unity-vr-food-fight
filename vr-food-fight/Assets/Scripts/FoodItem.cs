using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ThrowableObject
{
   
    [SerializeField] private string soundName;
    private Vector3 originalFruitPosition;
    private Transform parent;

    private FruitSpawner fruitSpawner;

    private void Awake()
    {
        fruitSpawner = FindObjectOfType<FruitSpawner>();
    }

    public override void OnGrabStart(XRHand hand)
    {
        base.OnGrabStart(hand);
        // modification
        originalFruitPosition = transform.position;
        parent = this.parent.transform;
    }
    
    public override void OnGrabEnd()
    {
        base.OnGrabEnd();
        fruitSpawner.SpawnNewFruit(originalFruitPosition, parent);
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Target"))
        {
            
            audioManager.Play("gong");
            FindObjectOfType<Target>().canMove = false;
            collision.rigidbody.isKinematic = false;
            collision.rigidbody.useGravity = true;
            Destroy(gameObject, 6f);
            Destroy(collision.gameObject, 3);
            // if(collision.gameObject == null)
            Invoke("BuildNewTarget", 4f);
            
        }
        
        if (collision.transform.CompareTag("Ground"))
        {
            audioManager.Play(soundName);
            Destroy(gameObject, 5);
        }
        
    }

    private void BuildNewTarget()
    {
        Debug.Log("1234");
        FindObjectOfType<TargetSpawner>().SpawnNewTarget();
    }

}