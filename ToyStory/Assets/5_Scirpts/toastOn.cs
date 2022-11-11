using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toastOn : MonoBehaviour
{
    bool toast;
    private Rigidbody rb;
    public PlayerController PlayerController;
    GameObject Toast;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Toast = GameObject.FindGameObjectWithTag("Toast");
        PlayerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if(toast && !PlayerController.isGrab)
        {
            if(PlayerController.Handed == false)
            {
                rb.isKinematic = toast;
                transform.SetParent(Toast.transform);
                transform.localPosition = new Vector3(0,0,0);
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    private void OnTriggerEnter(Collider point)
    {
        if(point.tag == "Toast")
        {
            toast = true;
        }
    }
}
