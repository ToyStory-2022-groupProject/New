using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    Animator _animator;
    void Update()
    {
        transform.position = player.position + offset;
        _animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        Debug.Log("이걸 실행 됨?");
        _animator.SetTrigger("doCamera");
    }
}
