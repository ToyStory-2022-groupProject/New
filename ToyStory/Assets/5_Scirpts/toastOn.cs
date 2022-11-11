using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toastOn : MonoBehaviour
{
    public GameObject brown;
    public GameObject puzzleBread;
    public GameObject answerBread;

    private bool isflying; // 캔디 공중에 떠있음
    // bool toast;
    private Rigidbody rb;
    // public PlayerController PlayerController;
    // GameObject Toast;
    
    void Start()
    {
        rb = brown.GetComponent<Rigidbody>();
        //Toast = GameObject.FindGameObjectWithTag("Toast");
        //PlayerController = FindObjectOfType<PlayerController>();
    }
    // void Update()
    // {
    //     if(toast && !PlayerController.isGrab)
    //     {
    //         if(PlayerController.Handed == false)
    //         {
    //             rb.isKinematic = toast;
    //             transform.SetParent(Toast.transform);
    //             transform.localPosition = new Vector3(0,0,0);
    //             transform.rotation = new Quaternion(0, 0, 0, 0);
    //         }
    //     }
    // }


    private void OnTriggerEnter(Collider point)
    {
        if(point.gameObject == puzzleBread && isflying)
        {
            puzzleBread.SetActive(false);
            answerBread.SetActive(true);
            ToasterSwitch.isInBrown = false;
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == brown)
        {
            isflying = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == brown)
        {
            isflying = true;
        }
    }
}
