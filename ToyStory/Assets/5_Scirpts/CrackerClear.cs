using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerClear : MonoBehaviour
{
    public CheckingPuzzle[] answers;
    public int count;
    bool _clear;

    void Update()
    {
        Complete();
        if (_clear)
        {
            StartCoroutine("End");
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < count; i++)
        {
            Destroy(answers[i].floor);
            Destroy(answers[i].Candy);
        }

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
        _clear = true;
    }
}
