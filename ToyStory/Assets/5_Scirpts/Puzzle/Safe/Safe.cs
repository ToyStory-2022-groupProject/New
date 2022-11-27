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

    public bool safeClear;

    private void Update()
    {
        if (isSafePuzzleClear)
        {
            isSafePuzzleClear = false;
            SafeOpen();
        }
    }

    public void SafeOpen()
    {
        audioSource.Play();
        StartCoroutine(Open());
        capsuleCollider.enabled = false;
        boxCollider.enabled = false;
        noiseCheck.SetActive(false);
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
