using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerManager : MonoBehaviour
{
    public static Action<SpinnerManager, SpinnerManager> scaleAdj;
    public static bool isChange;
    public GameObject realSpinner;
    
    public int spinNum;
    private bool isSpinning;
    private Vector3 scaleValue;
    public AudioSource audioSource;


    private void Start()
    {
        spinNum = 0;
        isSpinning = true;
        isChange = true;
        scaleValue = new Vector3(2f, 2f, 2f);
        scaleAdj = (SpinnerManager uiBefore, SpinnerManager uiCurrent) =>
        {
            if (isChange)
            {
                StartCoroutine(SpinnerScailng(uiBefore, uiCurrent));
            }
        };
    }

    public void Spin(int dir)
    {
        if (isSpinning)
        {
            audioSource.Play();
            StartCoroutine(CoroutineRotate(dir));
        }
    }

    IEnumerator CoroutineRotate(int dir)
    {
        isSpinning = false;
        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 0f, dir * -3f);
            realSpinner.transform.Rotate(0f, 0f, dir * -3f);;
            yield return YieldInstructionCache.WaitForSeconds(0.01f);
        }

        isSpinning = true;

        spinNum += 1 * dir;
        
        if (spinNum > 9)
        {
            spinNum = 0;
        }

        if (spinNum < 0)
        {
            spinNum = 9;
        }
    }
    
    IEnumerator SpinnerScailng(SpinnerManager uiBefore, SpinnerManager uiCurrent)
    {
        isChange = false;
        
        for (int i = 0; i <= 11; i++)
        {
            uiBefore.transform.localScale -= scaleValue;
            uiCurrent.transform.localScale += scaleValue;
            yield return YieldInstructionCache.WaitForSeconds(0.01f);
        }

        isChange = true;
    }
}
