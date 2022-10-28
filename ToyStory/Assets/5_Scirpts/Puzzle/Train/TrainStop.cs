using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TrainStop : MonoBehaviour
{
    [SerializeField] GameObject[] trains;
    [SerializeField] GameObject railWay;

    public float trainOperatingTime;
    private float speed;
    private float length;
    // private float currentTime;
    
    private void Start()
    {
        length = railWay.GetComponent<CinemachinePath>().PathLength; // 레일(Dolly track)의 길이
        speed = trains[0].GetComponent<CinemachineDollyCart>().m_Speed; // 기차(Cart) 속도
        trainOperatingTime = (length - trains[0].GetComponent<CinemachineDollyCart>().m_Position) / speed; // 시간 = 거리 / 시간
        Debug.Log("예상 시간 : " + trainOperatingTime);
        // currentTime = 0f;
    }
    
    // private void Update()
    // {
    //     if (currentTime >= trainOperatingTime)
    //     {
    //         ScriptOff();
    //     }
    //     else
    //     {
    //         currentTime += Time.deltaTime;
    //     }
    // }
    
    public void ScriptOn() // 기차 움직이게 하기
    {
        for (int i = 0; i < trains.Length; i++)
        {
            trains[i].GetComponent<CinemachineDollyCart>().enabled = true;
        }
    }
    
    public void ScriptOff() // 기차 멈추게 하기
    {
        for (int i = 0; i < trains.Length; i++)
        {
            trains[i].GetComponent<CinemachineDollyCart>().enabled = false;
        }
    } 
    
}
