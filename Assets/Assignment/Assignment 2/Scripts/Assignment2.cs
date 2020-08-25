using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Assignment2 : MonoBehaviour
{
    public bool floatInSpace = true;
    private Rigidbody myRigidBody = null;
    private Vector3 counterForce = Vector3.zero;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (floatInSpace)
        {
            FloatinSpace();
        }
        else
        {
            CounterGravity();
        }
    }

    private void FloatinSpace()
    {
        counterForce -= Physics.gravity.magnitude * Time.deltaTime * myRigidBody.velocity * myRigidBody.mass;
        myRigidBody.AddForce(counterForce);
    }

    private void CounterGravity()
    {
        myRigidBody.AddForce(-Physics.gravity * myRigidBody.mass);
    }
}
