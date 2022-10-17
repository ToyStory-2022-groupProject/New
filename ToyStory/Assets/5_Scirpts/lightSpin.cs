using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSpin : MonoBehaviour
{
    
    [SerializeField] float ySpeed = 1.0f;
    [SerializeField] GameObject Detection;
    private float time = 0.0f;

    void Start()
    {
        StartCoroutine(LightSpinning());
    }
    
    IEnumerator LightSpinning()
    {
        while(true)
        {
            while(time < 15f)
            {
                time += Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
                transform.Rotate(0, -ySpeed * Time.deltaTime * 5, 0);
                Detection.transform.Rotate(0, -ySpeed * Time.deltaTime * 5, 0);
                Debug.Log(time);
            }
            while(time > 15f && time < 30f)
            {
                time += Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
                transform.Rotate(0, ySpeed * Time.deltaTime * 5, 0);
                Detection.transform.Rotate(0, ySpeed * Time.deltaTime * 5, 0);
                Debug.Log(time);
            }
            time = 0;
        }
    }
}
