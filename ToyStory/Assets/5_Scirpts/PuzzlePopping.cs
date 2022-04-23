using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePopping : MonoBehaviour
{
    [SerializeField] float rise;
    Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void acting()
    {
        _rigidbody.AddForce(Vector3.up * rise, ForceMode.Force);
    }

    public void deactivating()
    {
        
    }
}
