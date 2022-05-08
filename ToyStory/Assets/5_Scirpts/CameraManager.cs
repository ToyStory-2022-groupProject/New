using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] Vector3 offset;
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
        transform.position = player.transform.position + offset;
        // _animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        _animator.SetTrigger("doCamera");
        // 나중에 카메라 애니메이션 관련
        // Debug.Log(other.gameObject.name);
        // if (other.gameObject.layer == 7)
        // {
        //     //CameraManager cam = cams.GetComponent<CameraManager>();
        //     //cam.Anim();
        // }
    }

    void CameraRotate() // 카메라 회전
    {
        Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
        
        if (Input.GetKey(KeySetting.keys[KeyAction.CAMDOWN]) || Input.GetKey(KeySetting.keys[KeyAction.CAMUP]))
        {
            rot.x += -1 * speed * (Input.GetKey(KeySetting.keys[KeyAction.CAMDOWN]) ? -1 : 1);
            Quaternion q = Quaternion.Euler(rot); // Quaternion으로 변환
            if(transform.rotation.x > -0.003f && transform.rotation.x < 0.3f) // 범위 지정
                transform.rotation = Quaternion.Slerp(transform.rotation, q, 0.1f); // 자연스럽게 회전
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initCameraRotate, 0.1f);
        }
    }
}
