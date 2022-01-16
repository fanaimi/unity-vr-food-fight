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

    private float walkingRange = 8f;
    private float runningRange = 4;

    private Rigidbody zrb;

    // Start is called before the first frame update
    void Start()
    {
        zrb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateToFollow();
        CheckDistance();
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
            if (m_distanceFromPrey <= runningRange)
            {
                m_animator.SetBool("Running", true);
            }
            else
            {
                m_animator.SetBool("Running", false);
            }

            approaching = true;
            m_animator.SetBool("Following", true);
            // MoveTowardsPrey();
        }
        else if(m_distanceFromPrey >= walkingRange)
        {
            approaching = false;
            m_animator.SetBool("Following", false);
            m_animator.SetBool("Running", false);
        }

    } // checkDistance

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



}
