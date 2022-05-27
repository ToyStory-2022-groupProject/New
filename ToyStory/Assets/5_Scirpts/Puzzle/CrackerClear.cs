using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerClear : MonoBehaviour
{
    public CheckingPuzzle[] answers;
    public GameObject effect;
    AudioSource _audioSource;
    public int count;
    bool isclear;
    bool isWork;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isWork)
        {
            Complete();
        }
        if (isclear)
        {
            isclear = false;
            effect.SetActive(true);
            _audioSource.Play();
            StartCoroutine("End");
        }
    }

    IEnumerator End()
    {
        //yield return new WaitForSeconds(2);
        for (int i = 0; i < count; i++)
        {
            Destroy(answers[i].floor);
            Destroy(answers[i].Candy);
        }
        yield return new WaitForSeconds(3);
        Destroy(effect);
        yield return null;
    }

    void Complete()
    {
        for (int i = 0; i < count; i++)
        {
            if (!answers[i].check)
            {
                return;
            }
        }

        isWork = true;
        isclear = true;
    }
}