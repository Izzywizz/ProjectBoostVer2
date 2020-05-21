using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // TODO Fix lighting issue
    [SerializeField]
    private float rcsThrust = 100.0f;

    [SerializeField]
    private float mainThrust = 10.0f;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private enum State
    {
        Alive, 
        Dying, 
        Transcending,
    }

    private State state = State.Alive;

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
        // TODO Stop sound on Death
        if (state == State.Alive)
        {
            Thurst();
            Rotate();
        }


    }


    /// <summary>
    /// Thrust
    /// </summary>
    private void Thurst()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!_audioSource.isPlaying) // So it doesnt layer the sound on top of eachotherS
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void Rotate()
    {
        // take manual control of rotation
        _rigidbody.freezeRotation = true;
        // modify rotation speed
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            // anti clockwise
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // clockwise - recall thumb is pointing at you
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        // resume phsuics control of rotation
        _rigidbody.freezeRotation = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        // prevent multiple collision calls as we are already dead
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 2.0f);
                break;

            default:
                Debug.Log("Dead");
                state = State.Dying;
                Invoke("LoadFirstLevel", 2.0f);
                break;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void LoadNextLevel()
    {
        // TODO allow for more levels

        SceneManager.LoadScene(1);
    }


    /// <summary>
    /// 
    /// </summary>
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}
