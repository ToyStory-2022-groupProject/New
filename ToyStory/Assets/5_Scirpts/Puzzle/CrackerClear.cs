using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerClear : MonoBehaviour
{
    public CheckingPuzzle[] answers;
    public GameObject effect;
    AudioSource _audioSource;
    public AudioSource footSound;
    public AudioSource fanSound;
    public GameObject door;
    public GameObject witch;
    public Vector3 witchTargetPos;
    public Vector3 speed;
    public int count;
    bool isclear;
    bool isWork;
    private bool isasd;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        speed = Vector3.zero;
        _rigidbody = witch.GetComponent<Rigidbody>();
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

            StopSound();
            door.transform.rotation = Quaternion.Euler(0, 45, 0);
            isasd = true;
            //StartCoroutine("End");
        }

        if (isasd)
        {
            _rigidbody.AddForce(Vector3.forward * -1, ForceMode.Impulse);
        }
    }

    // IEnumerator End()
    // {
    //     //yield return new WaitForSeconds(2);
    //     effect.SetActive(true);
    //     _audioSource.Play();
    //     for (int i = 0; i < count; i++)
    //     {
    //         Destroy(answers[i].floor);
    //         Destroy(answers[i].Candy);
    //     }
    //     yield return new WaitForSeconds(3);
    //     Destroy(effect);
    // }

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
        //footSound.Stop();
        fanSound.Stop();
    }
    
    private void OpenDoor()
    {
        door.transform.rotation = Quaternion.Euler(0, 45, 0);
    }
    
}