using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    void Update()
    {
        
    }

    public static float windspeed;
    public GameObject winds;
    Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate () 
    {
        _rigidbody.AddForce(winds.transform.forward * windspeed,ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == 7)
        {
            //CameraManager cam = cams.GetComponent<CameraManager>();
            //cam.Anim();
        }
    }
}
