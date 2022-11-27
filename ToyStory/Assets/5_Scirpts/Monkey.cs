using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Monkey : MonoBehaviour
{
    public CheckSight CheckSight;
    private Animator anim;
    private Chaser chaser;
    AudioSource AudioSource;
    
    void Start()
    {
        chaser = GetComponent<Chaser>();
        anim = GetComponent<Animator>();
        AudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(CheckSight.isDetected && chaser.stopDetect == false)
        {
            anim.SetBool("detect", CheckSight.isDetected);
            if(!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
        else
        {
            anim.SetBool("detect", CheckSight.isDetected);
            AudioSource.Stop();
        }
    }
}
