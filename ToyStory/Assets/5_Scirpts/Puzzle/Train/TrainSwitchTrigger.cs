using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TrainSwitchTrigger : MonoBehaviour
{
    public TrainLighting trainLighting;
    public PlayableDirector trainSwitchOnPD;
    public TimelineAsset trainSwitchOnTimeline;
    public GameObject switchLight; // 스위치 불빛
    public bool isTrigger;

    public PlayerController _playerController;

    private void OnTriggerStay(Collider other)
    {
        if (PlayerController.isGrab && other.gameObject.layer == 3 && _playerController.Handed == false)
        {
            trainLighting.isOperate = true;
            isTrigger = true;
            trainSwitchOnPD.Play(trainSwitchOnTimeline);
            switchLight.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
