using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidbody;

    /// Start is called before the first frame update
    void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }


    /// Update is called once per frame
    void Update()
    {
        ProcessInput();
    }


    /// <summary>
    /// Handles Rotation and Thrust input
    /// </summary>
    private void ProcessInput()
    {
        // Thrust
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up);
        }

        // Rotation
        if (Input.GetKey(KeyCode.A))
        {
            // anti clockwise
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // clockwise - recall thumb is pointing at you
            transform.Rotate(-Vector3.forward);
        }
    }
}
