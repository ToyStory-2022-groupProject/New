using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //public Transform player;
    //public Vector3 offset;
    [SerializeField] float speed;
    Quaternion initCameraRotate;
    Animator _animator;

    float h;

    void awake()
    {
        initCameraRotate = transform.rotation;
    }
    
    void Update()
    {
        Debug.Log(Convert.ToInt32(h));
        CameraRotate();
        // transform.position = player.position + offset;
        // _animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        _animator.SetTrigger("doCamera");
    }

    void CameraRotate() // 카메라 회전
    {
        if (Input.GetKey(KeyCode.S))
        {
            if(h < 0.5)
                h += 0.1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (h > -0.5)
                h -= 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            transform.rotation = initCameraRotate;
        }
        h = speed * h * Time.deltaTime;
        transform.Rotate(Vector3.right * h);
    }
}
