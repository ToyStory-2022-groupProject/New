using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSpin : MonoBehaviour
{
    
    [SerializeField] float ySpeed = 1.0f;
    [SerializeField] GameObject Detection;

    void Start()
    {
        StartCoroutine(LightSpinning(0.01f, 0.01f));
    }
    
    IEnumerator LightSpinning(float left, float right)
    {
        float count = 0;
        while(true)
        {
            while(count < 4f)
            {
                count += left;
                yield return new WaitForSeconds(0.01f);
                transform.Rotate(0, -ySpeed * Time.deltaTime * 5, 0);
                Detection.transform.Rotate(0, -ySpeed * Time.deltaTime * 5, 0);
            }
            while(count > 0.0f)
            {
               count -= right;
                yield return new WaitForSeconds(0.01f);
                transform.Rotate(0, ySpeed * Time.deltaTime * 5, 0);
                Detection.transform.Rotate(0, ySpeed * Time.deltaTime * 5, 0);
            }
        }
    }
}
