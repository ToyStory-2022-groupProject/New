using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [Header("물 관련")]
    [Tooltip("부력 세기")]
    [SerializeField] float floatingPower;
    Rigidbody _rigidbody;
    
    
    public GameObject water;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    
    void OnTriggerStay(Collider other) // 물에 닿았을 때
    {
        if (other.gameObject.layer == 4 && gameObject.layer == 8)
        {
            _rigidbody.AddForce(Vector3.up * floatingPower, ForceMode.Acceleration);
        }
    }
    
}
