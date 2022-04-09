using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    
    public GameObject cams;
    // Vector3 sphere;
    // Vector3 currentSpeed;
    // Rigidbody _rigidbody;
    
    // public float speed;
    // float h;
    // float v;
    
    // bool isBorder;
    // void Awake()
    // {
    //     _rigidbody = GetComponent<Rigidbody>();
    // }

    void Update()
    {
        Pause();
        Menu();
        // Move();
        // Turn();
    }

    void Pause() // 게임 정지
    {
        var obj = FindObjectOfType<SubUI>();
        Time.timeScale = (obj == null) ? 1 : 0;
    }
    // void Move()
    // {
    //     h = Input.GetAxisRaw("Horizontal");
    //     v = Input.GetAxisRaw("Vertical");
    //     sphere = new Vector3(h, 0, v).normalized;
    //     if(!isBorder)
    //         transform.position += sphere * Time.deltaTime * speed * (isBorder ? 0.3f : 1);
    // }
    // void Turn()
    // {
    //     // 키보드에 의한 회전
    //     transform.LookAt(transform.position + sphere);
        
    // }

    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SubUI.Instance.LoadSubMenu();
        }
    }

    // void FixedUpdate()
    // {
    //     StopToWall(); 
    // }

    // void StopToWall()
    // {
    //     Debug.DrawRay(transform.position, transform.forward * 15,  Color.magenta);
    //     isBorder = Physics.Raycast(transform.position,
    //         transform.forward, 15, LayerMask.GetMask("Wall"));
    // }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == 7)
        {
            CameraManager cam = cams.GetComponent<CameraManager>();
            cam.Anim();
        }
    }
}
