using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Rigidbody doorRigid;
    public GameObject key;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            doorRigid.isKinematic = false;
            key.SetActive(false);
        }
    }
}
