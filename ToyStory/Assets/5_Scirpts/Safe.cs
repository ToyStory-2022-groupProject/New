using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public static bool isSafePuzzle;
    public static bool isSafePuzzleClear;
    public GameObject passwardUI;
    public GameObject panel;
    public GameObject SafeDoor;
    public PlayerController playerController;
    public AudioSource audioSource;
    public CapsuleCollider capsuleCollider;
    public BoxCollider boxCollider;

    private void Update()
    {
        if (isSafePuzzleClear)
        {
            isSafePuzzleClear = false;
            SafeOpen();
        }
    }

    void SafeOpen()
    {
        audioSource.Play();
        StartCoroutine(Open());
        capsuleCollider.enabled = false;
        boxCollider.enabled = false;
    }

    IEnumerator Open()
    {
        for (int i = 0; i < 50; i++)
        {
            SafeDoor.transform.localPosition += new Vector3(-0.01f, 0f, 0f);
            yield return YieldInstructionCache.WaitForSeconds(0.01f);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (PlayerController.isGrab && other.gameObject.layer == 3)
        {
            passwardUI.SetActive(true);
            panel.SetActive(true);
            playerController.enabled = false;
            isSafePuzzle = true;
            PlayerController.isGrab = false;
        }
    }
}
