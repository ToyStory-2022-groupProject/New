using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Monkey : MonoBehaviour
{
    public CheckSight CheckSight;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(CheckSight.isDetected)
        {
            anim.SetBool("detect", CheckSight.isDetected);
            //SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Cymbals);
            Debug.Log("play");
        }
        else
        {
            anim.SetBool("detect", CheckSight.isDetected);
            //SFXMgr.Instance.Stop_SFX();
            Debug.Log("playStop");
        }
    }
}
