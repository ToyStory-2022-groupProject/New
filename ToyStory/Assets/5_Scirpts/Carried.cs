using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Carried : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerController PlayerController;
    GameObject Hand;
    bool OnHand;
    int layerNum;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Hand = GameObject.FindGameObjectWithTag("Hand");
        PlayerController = FindObjectOfType<PlayerController>();
        layerNum = gameObject.layer;
    }
    
   
    void Update()
    {
        if(OnHand && PlayerController.isGrab)
        {
            if(PlayerController.Handed == false)
            {
                rb.isKinematic = OnHand;
                transform.SetParent(Hand.transform);
                PlayerController.Handed = OnHand;
                gameObject.layer = 16;
            }
        }
        else if(!PlayerController.isGrab)
        {
            OnHand = PlayerController.Handed = false;
            Hand.transform.DetachChildren();
            rb.isKinematic = OnHand;
            gameObject.layer = layerNum;
        }
    }

    private void OnTriggerEnter(Collider point)
    {
        if(point.tag == "Hand")
        {
            OnHand = true;
        }
    }
    // private void OnTriggerExit(Collider point)
    // {
    //     if(point.tag == "Hand")
    //     {
    //         transform.rotation = new Quaternion(0, 0, 0, 0);
    //     }
    // }


}
