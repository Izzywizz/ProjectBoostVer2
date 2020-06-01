using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // TODO Fix lighting issue
    [Header("Thrust Settings")]
    [SerializeField]
    private float rcsThrust = 100.0f;

    [SerializeField]
    private float mainThrust = 10.0f;

    [Header("Sound")]
    [SerializeField]
    private AudioClip _mainEngine = null;

    [SerializeField]
    private AudioClip _death = null;

    [SerializeField]
    private AudioClip _success = null;

    [Header("Particle System")]
    [SerializeField]
    ParticleSystem _mainEngineParticleSystem = null;

    [SerializeField]
    ParticleSystem _deathParticleSystem = null;

    [SerializeField]
    ParticleSystem _successParticleSystem = null;

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
            RespondToThurstInput();
            RespondToRotate();
        }


    }


    /// <summary>
    /// Thrust
    /// </summary>
    private void RespondToThurstInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            _audioSource.Stop();
            _mainEngineParticleSystem.Stop();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void ApplyThrust()
    {
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // Prevent the sound from layering another sound on top
        if (!_audioSource.isPlaying && _mainEngine != null) 
        {
            if (state == State.Alive)
            {
                _audioSource.PlayOneShot(_mainEngine);
            }
        }
        _mainEngineParticleSystem.Play();
    }


    /// <summary>
    /// 
    /// </summary>
    private void RespondToRotate()
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
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void StartSuccessSequence()
    {
        state = State.Transcending;
        _audioSource.Stop();
        PlayLoadNewLevelSound();
        _successParticleSystem.Play();
        Invoke("LoadNextLevel", 1.0f);
    }


    /// <summary>
    /// 
    /// </summary>
    private void StartDeathSequence()
    {
        state = State.Dying;
        _audioSource.Stop();
        PlayDeathSound();
        _deathParticleSystem.Play();
        Invoke("LoadFirstLevel", 1.0f);
    }


    /// <summary>
    /// 
    /// </summary>
    private void PlayLoadNewLevelSound()
    {
        if (_audioSource != null && _success != null)
        {
            _audioSource.PlayOneShot(_success);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void PlayDeathSound()
    {
        if (_audioSource != null && _death != null)
        {
            _audioSource.PlayOneShot(_death);
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
