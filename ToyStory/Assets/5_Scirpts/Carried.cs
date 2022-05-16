using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Carried : MonoBehaviour
{
    private BoxCollider col;
    private Rigidbody rb;
    GameObject Hand;
    bool OnHand;
    void Start()
    {
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        Hand = GameObject.FindGameObjectWithTag("Hand");
    }
   
    void Update()
    {
        if(OnHand && PlayerController.isGrab)
        {
            Debug.Log("Grab");
            rb.isKinematic = OnHand;
            transform.SetParent(Hand.transform);

        }
        else if(!PlayerController.isGrab)
        {
            OnHand = false;
            Hand.transform.DetachChildren();
            rb.isKinematic = OnHand;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touch");
            OnHand = true;
        }
    }

}
