using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Monkey : MonoBehaviour
{
    public CheckSight CheckSight;
    private Animator anim;

    AudioSource AudioSource;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        AudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(CheckSight.isDetected)
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
