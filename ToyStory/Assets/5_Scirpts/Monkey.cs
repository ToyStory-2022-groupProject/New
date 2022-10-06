using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Monkey : MonoBehaviour
{
    public CheckSight checkSight;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        while(checkSight.isDetected)
        {
            anim.SetBool("detect", checkSight.isDetected);
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Cymbals);
        }
    }
}
