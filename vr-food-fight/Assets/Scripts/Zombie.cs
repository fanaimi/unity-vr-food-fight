using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform m_prey;
    [SerializeField] private Animator m_animator;

    private Quaternion m_lookRotation;
    [SerializeField] float RotationSpeed;
    private Vector3 m_direction;
    private Vector3 m_heading;

    private float m_distanceFromPrey;

    private float walkingRange = 3f;
    private float runningRange = 4;

    private Rigidbody zrb;

    // Start is called before the first frame update
    void Start()
    {
        zrb = GetComponent<Rigidbody>();
        // InvokeRepeating("Growl",3, 10);
    }

    void Growl()
    {
        Debug.Log("growl");
        AudioManager.instance.Play("growl");
    }

    // Update is called once per frame
    void Update()
    {
        RotateToFollow();
        // CheckDistance();
    }


    private void RotateToFollow()
    {
        // https://answers.unity.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html
        m_heading = m_prey.transform.position - transform.position;
        m_direction = m_heading.normalized;

        m_lookRotation = Quaternion.LookRotation(m_direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookRotation, Time.deltaTime * RotationSpeed);
    } // RotateToFollow


    private void CheckDistance()
    {
        // https://docs.unity3d.com/2017.4/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html
        m_distanceFromPrey = m_heading.magnitude;

        if ( m_distanceFromPrey < walkingRange)
        {
            Debug.Log(m_distanceFromPrey);
            
            /*if (m_distanceFromPrey <= runningRange)
            {
                m_animator.SetBool("Running", true);
            }
            else
            {
                m_animator.SetBool("Running", false);
            }*/

            approaching = true;
            // AudioManager.instance.Play("growl");
            Growl();
            m_animator.SetBool("Following", true);
            // MoveTowardsPrey();
        }
        else if(m_distanceFromPrey >= walkingRange)
        {
            approaching = false;
            m_animator.SetBool("Following", false);
            //m_animator.SetBool("Running", false);
        }

    } // checkDistance


    public void GetActive()
    {
        approaching = true;
        // AudioManager.instance.Play("growl");
        Growl();
        m_animator.SetBool("Following", true);
        // MoveTowardsPrey();
    }


    public void Sleep()
    {
        approaching = false;
        m_animator.SetBool("Following", false);
    }



    private bool approaching = false;
    [SerializeField] private float tempSpeed = .9f;
    private void MoveTowardsPrey()
    {
        if (approaching)
        {
            Vector3 velocity = transform.forward * tempSpeed;
        
        zrb.MovePosition(zrb.position + velocity * Time.deltaTime);
        }

    } // MoveTowardsPrey


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in");
            GetActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("out");
            Sleep();
        }
    }
    

}
