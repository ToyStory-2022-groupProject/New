using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TrainLighting : MonoBehaviour
{
     public PlayableDirector trainSwitchOffPD;
     public TimelineAsset trainSwitchOffTimeline;
     public GameObject switchTrigger;
     public GameObject[] bulbs; // 전구들
     public GameObject switchLight; // 스위치 불빛
     public Light[] lights; // 전구 불빛
     public TrainStop trainTime;
     public bool isOperate;
     public int cnt;
     public AudioSource audioSource;
     
     private bool isArrive;
     private bool isEndOperate;
     private float runningTime;
     public bool Connected;

     

     private void Start()
     {
         runningTime = trainTime.trainOperatingTime;
         cnt = 0;
     }

     // Update is called once per frame
     void Update()
     {
         if (switchTrigger.GetComponent<TrainSwitchTrigger>().isTrigger)
         {
             if (isOperate)
             {
                 audioSource.enabled = true;
                 StartCoroutine("Operate");
             }
             else // 전구 끄기
             {
                 audioSource.enabled = false;
                 StopCoroutine("Operate");
                 trainSwitchOffPD.Play(trainSwitchOffTimeline);
                 OffBulb(cnt);
                 switchTrigger.GetComponent<TrainSwitchTrigger>().isTrigger = false;
                 SwitchActive();
             }
         }
     }

     void SwitchActive()
     {
         switchLight.SetActive(false);
         switchTrigger.SetActive(true);
     }

     void OffBulb(int size)
     {
         for (int i = 0; i < size; i++)
         {
             lights[i].enabled = false;
         }
     }
     IEnumerator Operate()
     {
         yield return YieldInstructionCache.WaitForSeconds(1f);

         for (int i = 0; i < bulbs.Length; i++)
         {
             if (bulbs[i].activeSelf == false)
             {
                 isOperate = false;
                 cnt = i;
                 yield break; 
             }
             yield return YieldInstructionCache.WaitForSeconds(0.1f);
             lights[i].enabled = true;
             if (i == bulbs.Length - 1 && Connected == false)
             {
                 Connected = true;
                 cnt = i + 1;
             }
             
         }
         
         // 만약 레일의 모든 전구가 다 들어와 전기가 연결되었다면 기차 운행시키기
          if (Connected)
          {
              // 기차 출발
              if (isArrive == false)
              {
                  trainTime.ScriptOn();
                  yield return YieldInstructionCache.WaitForSeconds(trainTime.trainOperatingTime);
                  isArrive = true;
              }
              // 기차 도착
              else
              { 
                  trainTime.ScriptOff();
                  yield return YieldInstructionCache.WaitForSeconds(0.5f);
                  isOperate = false; 
              }
          }
     }
}
