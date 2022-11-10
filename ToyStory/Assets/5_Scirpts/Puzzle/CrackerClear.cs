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
        if (isclear)
        {
            isclear = false;
            for (int i = 0; i < count; i++)
            { 
                Destroy(answers[i].floor);
                Destroy(answers[i].Candy);
            }

            //StopSound();
            doorSource.Play();
            door.transform.rotation = Quaternion.Euler(0, 45, 0);
            witch.SetActive(true);
            witchController.isWitchMove = true;
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
    
    private void OpenDoor()
    {
        door.transform.rotation = Quaternion.Euler(0, 45, 0);
    }
}