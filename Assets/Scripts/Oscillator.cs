using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _movementVector = Vector3.zero;

    // Time it takes to do one full cycle
    [SerializeField]
    private float _period = 2.0f;

    private float _movementFactor = 0; //  0 - not moved, 1 - fully moved
    private Vector3 _startingPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        _startingPos = transform.position;        
    }


    // Update is called once per frame
    void Update()
    {
        // TODO Protect from period agasint zero
        // grow continually from zero
        if (_period < Mathf.Epsilon)
        {
            Debug.Log("Waring Period value is less than 0");
            return;
        }
        float cycles = Time.time / _period;

        const float tau = Mathf.PI * 2.0f; // mathmatically notation (6.8), all the way round the circle in radian
        float rawSinWave = Mathf.Sin(cycles * tau); // varies between -1 and +1

        _movementFactor = rawSinWave / 2.0f + 0.5f;
        Vector3 offset = _movementFactor * _movementVector;
        transform.position = _startingPos + offset;
    }
}
