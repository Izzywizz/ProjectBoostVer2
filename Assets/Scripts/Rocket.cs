using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    /// Start is called before the first frame update
    void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
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

            if (!_audioSource.isPlaying) // So it doesnt layer the sound on top of eachotherS
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
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
