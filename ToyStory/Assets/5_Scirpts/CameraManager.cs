using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speed;
    Quaternion initCameraRotate;
    Animator _animator;

    float h;

    void Awake()
    {
        initCameraRotate = transform.rotation;
    }
    
    void Update()
    {
        //Debug.Log(Convert.ToInt32(h)); 잠깐 꺼놓을게요
        CameraRotate();
        transform.position = player.position + offset;
        // _animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        _animator.SetTrigger("doCamera");
    }

    void CameraRotate() // 카메라 회전
    {
        Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            rot.x += -1 * speed * (Input.GetKey(KeyCode.S) ? -1 : 1);
            Quaternion q = Quaternion.Euler(rot); // Quaternion으로 변환
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 0.1f); // 자연스럽게 회전
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initCameraRotate, 0.1f);
        }
    }
}
