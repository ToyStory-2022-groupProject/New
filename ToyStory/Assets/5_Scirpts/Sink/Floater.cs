using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] float waveHeight;
    [SerializeField] float frequency;
    [SerializeField] float speed;
    [SerializeField] float waterSurface;
    public float floatSpeed;
    
    public GameObject water;
    public static bool isfull;
    bool isWet;
    Vector3 pos;
    float lerpTime;
    
    void FixedUpdate() {
        Vib();
    }

    void Vib()
    {
        if (isWet)
        {
            if (PlayerController.isGrab && !isfull)
            {
                transform.Translate(Vector3.up * Time.deltaTime * floatSpeed);
                //transform.position += new Vector3(0, 0.02f, 0);
            }
            else
            {
                float y = Mathf.PingPong(Time.time * speed, frequency) * waveHeight;
                transform.position = new Vector3(transform.position.x, water.transform.position.y - waterSurface + y, transform.position.z);
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            if (other.gameObject.layer == 8)
            {
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.useGravity = false;
            }
            isWet = true;
        }
    }
}
