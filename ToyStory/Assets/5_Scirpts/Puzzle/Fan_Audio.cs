using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fan_Audio : MonoBehaviour
{
	[Header("오디오")] 
	[SerializeField] AudioClip[] audioclips;
	AudioSource audioSource;
	static public bool isSwitchOn;
	
	bool isSwitchPlay;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		StartCoroutine("PlayAudio");
	}

	IEnumerator PlayAudio()
	{
		if (!isSwitchPlay && isSwitchOn)
		{
			Debug.Log("스위치 재생");
			audioSource.clip = audioclips[0];
			audioSource.Play();
			yield return new WaitForSeconds(1);
			isSwitchPlay = true;
		}
		else if (isSwitchPlay && isSwitchOn)
		{
			Debug.Log("바람 재생");
			audioSource.clip = audioclips[1];
			//audioSource.Play();
		}
	}
}
