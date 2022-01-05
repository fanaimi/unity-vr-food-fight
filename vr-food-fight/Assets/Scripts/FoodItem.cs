using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : ThrowableObject
{
   
    [SerializeField] private string soundName;
    [SerializeField] private Target targetPrefab;
    [SerializeField] private Vector3 targetPos = new Vector3(-5.5f, 2.8f, 6.6f);

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
            Destroy(gameObject, 5);
            Destroy(collision.gameObject, 5);
            if (Instantiate(targetPrefab, targetPos, Quaternion.identity))
            {
                Debug.Log("instantiated");
            }

            ;
        }
        
        if (collision.transform.CompareTag("Ground"))
        {
            audioManager.Play(soundName);
            Destroy(gameObject, 5);
        }
        
    }
}