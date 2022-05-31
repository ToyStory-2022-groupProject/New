using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Carried : MonoBehaviour
{
    
    private Rigidbody rb;
    GameObject Hand;
    bool OnHand;
    int layerNum;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Hand = GameObject.FindGameObjectWithTag("Hand");
        layerNum = gameObject.layer;
    }
   
    void Update()
    {
        if(OnHand && PlayerController.isGrab)
        {
            Debug.Log("Grab");
            rb.isKinematic = OnHand;
            transform.SetParent(Hand.transform);
            gameObject.layer = 16;
        }
        else if(!PlayerController.isGrab)
        {
            OnHand = false;
            Hand.transform.DetachChildren();
            rb.isKinematic = OnHand;
            gameObject.layer = layerNum;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnHand = true;
        }
    }

}
