using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ThrowableObject
{
   
    [SerializeField] private string soundName;

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

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