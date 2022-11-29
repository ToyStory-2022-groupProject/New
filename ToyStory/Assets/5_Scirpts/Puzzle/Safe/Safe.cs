using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public static bool isSafePuzzle; // UI관련
    public static bool isSafePuzzleClear;
    public GameObject passwardUI;
    public GameObject panel;
    public GameObject SafeDoor;
    public AudioSource audioSource;
    public CapsuleCollider capsuleCollider;
    public BoxCollider boxCollider;
    public Cat cat;
    public GameObject noiseCheck;
    public BoxCollider checkPointer7;
    public bool safeClear;

    public AudioSource clearMusic;
    public GameObject stage4Key;    
    public Rigidbody doorRigid4;

    Vector3 k4position;
    Vector3 k4Roatation;
    Vector3 doorOriginPos;
    Vector3 doorOriginRot;

    void Start(){
        doorOriginPos = SafeDoor.transform.position;
        doorOriginRot = SafeDoor.transform.eulerAngles;

        k4position = stage4Key.transform.position;
        k4Roatation = stage4Key.transform.eulerAngles;
    }

    private void Update()
    {
        if (isSafePuzzleClear)
        {
            isSafePuzzleClear = false;
            SafeOpen();
        }
    }

    public void SafeOpen(bool isGameOver = false)
    {
        if(Cat.isCatSpurned == false)
        {
            isSafePuzzle = false;
            audioSource.Play();
        }
        if(isGameOver == false){
            capsuleCollider.enabled = false;
            boxCollider.enabled = false;
            checkPointer7.enabled = true;
            noiseCheck.SetActive(false);
        }
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        for (int i = 0; i < 50; i++)
        {
            SafeDoor.transform.localPosition += new Vector3(-0.01f, 0f, 0f);
            if (i == 49)
            {
                cat.isfound = true;
                cat.playerRun = true;
            }
            yield return null;
        }
    }
    
    public void InitSafe()
    {
        isSafePuzzle = false;
        SafeDoor.transform.position = doorOriginPos;
        SafeDoor.transform.eulerAngles = doorOriginRot;
        stage4Key.transform.position = k4position;
        stage4Key.transform.eulerAngles = k4Roatation;
        if(stage4Key.activeSelf == false)
        {
            stage4Key.SetActive(true);
            doorRigid4.isKinematic = true;
        }
        clearMusic.Play();
        SafeOpen();
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerController.isGrab && other.gameObject.layer == 3)
        {
            passwardUI.SetActive(true);
            panel.SetActive(true);
            noiseCheck.SetActive(false);
            isSafePuzzle = true;
        }
    }
}
