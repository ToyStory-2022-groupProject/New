using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerClear : MonoBehaviour
{
    public CheckingPuzzle[] answers;
    public GameObject effect;
    public GameObject witch;
    public AudioSource doorSource;
    public AudioSource footSound;
    public AudioSource fanSound;
    public GameObject door;
    public Vector3 speed;
    public int count;
    bool isclear;
    bool isWork;
    public WitchController witchController;
    private Rigidbody _rigidbody;

    
    private void Start()
    {
        speed = Vector3.zero;
    }

    void Update()
    {
        if (!isWork)
        {
            Complete();
        }
        Debug.Log(isclear + "ASD");
        if (isclear)
        {
            isclear = false;
            for (int i = 0; i < count; i++)
            { 
                answers[i].floor.SetActive(false);
                answers[i].Candy.SetActive(false);
            }
            Debug.Log("마녀랑 문이 열려야 하는데? " + DataManager.PointNum);
            //StopSound();
            if(DataManager.PointNum >= 1)
            {
                Debug.Log("마녀랑 문이 열려야 하는데? " + isclear);
                door.transform.rotation = Quaternion.Euler(0, 45, 0);

                if(DataManager.PointNum == 1)
                {
                    doorSource.Play();
                    witch.SetActive(true);
                    witchController.isWitchMove = true;
                }
            }
            // doorSource.Play();
            // door.transform.rotation = Quaternion.Euler(0, 45, 0);
            // witch.SetActive(true);
            // witchController.isWitchMove = true;
            //StartCoroutine("End");
        }
        
    }

    void Complete()
    {
        for (int i = 0; i < count; i++)
        {
            if (!answers[i].check)
            {
                return;
            }
        }

        isWork = true;
        isclear = true;
    }

    public void PlaySound()
    {
        footSound.Play();
        fanSound.Play();
    }
    
    public void StopSound()
    {
        footSound.Stop();
        fanSound.Stop();
    }
    
}