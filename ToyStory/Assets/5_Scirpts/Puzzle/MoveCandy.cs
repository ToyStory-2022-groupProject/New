using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCandy : MonoBehaviour
{
    public Vector3  targetPos;
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, targetPos, 1f * Time.deltaTime);
    }
}
