using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CameraConversion : MonoBehaviour
{
    public PlayableDirector pastToNextPD;
    public PlayableDirector nextToPastPD;
    public TimelineAsset pastToNextTimelines;
    public TimelineAsset nextToPastTimelines;

    private bool isPastPlayed;
    private bool isNextPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isPastPlayed == false)
        {
            pastToNextPD.Play(pastToNextTimelines);
            isPastPlayed = true;
            isNextPlayed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isNextPlayed == false)
        {
            nextToPastPD.Play(nextToPastTimelines);
            isPastPlayed = false;
            isNextPlayed = true;
        }
    }
}
