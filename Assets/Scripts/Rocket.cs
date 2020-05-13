using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    /// Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("Thrust");
        }

        // Rotation
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating Right");
        }
    }
}
