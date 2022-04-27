using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
        {
            Debug.Log("왼쪽");
            rb.AddForce(Vector3.back*1, ForceMode.Force);
        }
        else if(Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
        {
            Debug.Log("오른쪽");
            rb.AddForce(Vector3.forward*1, ForceMode.Force);            
        }
    }
}
