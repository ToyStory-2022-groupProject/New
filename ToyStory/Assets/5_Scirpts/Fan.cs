using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject wing;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere")
        {
            wing.SetActive(true);
            Sphere.windspeed = 1f;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Sphere")
        {
            Sphere.windspeed = 0f;
            wing.SetActive(false);
        }
    }
}
