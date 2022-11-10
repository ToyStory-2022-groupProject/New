using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class RewriteFan : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] TimelineAsset timeline;

    private bool isPlayed;
    private bool isWingRotate;
    public Animator anim;

    [Tooltip("선풍기 날개")]
    [SerializeField] GameObject wing; // 퍼즐 관련 도구
    
    void Update()
    {
        if(isWingRotate)
            wing.transform.Rotate(new Vector3(0,0,1) * Time.deltaTime*500);
    }  
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isPlayed)
        {
            playableDirector.Play(timeline);
            anim.SetBool("Move", false);
            isPlayed = true;
            isWingRotate = true;
        }
    }
}
