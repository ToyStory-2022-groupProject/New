using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Rigidbody doorRigid;
    public GameObject key;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == key)
        {
            doorRigid.isKinematic = false;
            _audioSource.Play();
            key.SetActive(false);
        }
    }
}
