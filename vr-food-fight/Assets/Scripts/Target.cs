using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float moveSpeed, moveAmount, spinupAmount, upDownSpeed;
    public bool canMove = true;

    private Vector3 startPosition;

    /// <summary>
    /// singleton start
    /// </summary>
    private static Target _instance;
    public static Target Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    /// <summary>
    /// singleton end
    /// </summary>
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            var newPosition = transform.position;
            
            newPosition.x = startPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveAmount;
            newPosition.y = startPosition.y + Mathf.Sin(Time.time * upDownSpeed) * spinupAmount;
    
            transform.position = newPosition;
        }
        

        
    }
}