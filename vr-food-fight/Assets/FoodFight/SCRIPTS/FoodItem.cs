using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ThrowableObject
{
   
    [SerializeField] private string soundName;
    private Vector3 originalFruitPosition;
    private Transform thisParent;

    // private FruitSpawner fruitSpawner;


    private void Awake()
    {
        // fruitSpawner = FindObjectOfType<FruitSpawner>();
    }

    public override void OnGrabStart(XRHand hand)
    {
        base.OnGrabStart(hand);
        // modification
        originalFruitPosition = transform.position;
    }
    
    public override void OnGrabEnd()
    {
        base.OnGrabEnd();
        if (isFruit)
        {
            fruitSpawner.SpawnNewFruit(originalFruitPosition);
            Destroy(gameObject, 5);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Target"))
        {
            FindObjectOfType<UiManager>().UpdateTargetsUI(FindObjectOfType<Target>().scoreGain);
            AudioManager.instance.Play("gong");
            FindObjectOfType<Target>().canMove = false;
            collision.rigidbody.isKinematic = false;
            collision.rigidbody.useGravity = true;
            if (GameManager.Instance.playing)
            {
                BuildNewTarget();
            }

            Destroy(gameObject, .5f);
            Destroy(collision.gameObject, 1.5f);
            // if(collision.gameObject == null)
            
        }
        
        if (collision.transform.CompareTag("Ground"))
        {
            AudioManager.instance.Play(soundName);
            Destroy(gameObject, 3);
        }
        
    }

    private void BuildNewTarget()
    {
        // Debug.Log("build new target");
        FindObjectOfType<TargetSpawner>().WaitToSpawn();
    }

}