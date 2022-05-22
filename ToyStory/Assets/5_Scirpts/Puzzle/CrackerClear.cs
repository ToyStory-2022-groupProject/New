using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerClear : MonoBehaviour
{
    public CheckingPuzzle[] answers;
    public GameObject effect;
    public int count;
    bool isclear;
    bool isWork;

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
