using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject wind;
    
    float windspeed;
    
    Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate () 
    {
        _rigidbody.AddForce(wind.transform.forward * windspeed,ForceMode.Impulse);
    }
    
    public void acting()
    {
        windspeed = 1f;
    }

    public void deactivating()
    {
        windspeed = 0;
        Destroy(gameObject);
    }
}
