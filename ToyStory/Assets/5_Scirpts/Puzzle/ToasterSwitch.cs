using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterSwitch : MonoBehaviour
{
    [SerializeField] GameObject candy;
    [SerializeField] float rise;
    [Header("오디오")]
    [SerializeField] AudioClip[] audioClips;
    AudioSource _audioSource;

    Rigidbody _rigidbody;
    bool _isSet;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = candy.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isSet)
        {
            StartCoroutine("StartTimer");
        }
    }

    IEnumerator StartTimer() // 토스터 퍼즐 타이머 작동
    {
        _isSet = false;
        _audioSource.clip = audioClips[0];
        _audioSource.Play();
        yield return new WaitForSeconds(8);
        _audioSource.clip = audioClips[1];
        _audioSource.Play();
        Popping();
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        yield return null;
    }
    
    void Popping()
    {
        _rigidbody.AddForce(Vector3.up * rise, ForceMode.Impulse);
    }
    void OnTriggerStay(Collider other)
    {
        if (PlayerController.isGrab && other.gameObject.layer == 3)
        {
            Debug.Log("눌렀다.");
            _isSet = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
