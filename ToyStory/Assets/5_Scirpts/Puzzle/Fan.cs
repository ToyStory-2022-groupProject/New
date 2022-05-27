using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    
    [Header("효과")]
    [SerializeField] GameObject effect;
    [Tooltip("선풍기 날개")]
    [SerializeField] GameObject wing; // 퍼즐 관련 도구
    [Tooltip("날려버릴 오브젝트")]
    [SerializeField] GameObject[] trashObject; // 퍼즐 관련 도구가 집합체일경우
    [SerializeField] Rigidbody[] _rigidbodys;

    float timer = 2.7f;
    float windspeed; // 바람 세기
    bool isSet;
    bool isComplete;
    
    void Update ()
    {
        if (isSet && !isComplete)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
                BlowUp();
        }
    }

    void BlowUp()
    {
        windspeed = 3f;
        for (int i = 0; i < trashObject.Length; i++)
        {
            _rigidbodys[i].AddForce(trashObject[i].transform.forward * windspeed,ForceMode.Impulse);
        }
        Invoke("CandyDestroy", 4f);
    }

    void CandyDestroy()
    {
        isComplete = true;
        for (int i = 0; i < trashObject.Length; i++)
        {
            Destroy(trashObject[i]);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isComplete)
        {
            Debug.Log("inEnter");
            isSet = true;
        }    
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("in");
            wing.transform.Rotate(new Vector3(0,0,1) * Time.deltaTime*500);
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Exit");
            effect.SetActive(false);
            wing.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 0);
        }
    }
}