using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _movementVector = Vector3.zero;

    // TODO Remove from Inspector later
    [Range(0,1)]
    [SerializeField]
    private float _movementFactor = 0; // 0 - not moved, 1 - fully moved

    private Vector3 _startingPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        _startingPos = transform.position;        
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 offset = _movementFactor * _movementVector;
        transform.position = _startingPos + offset;
    }
}
