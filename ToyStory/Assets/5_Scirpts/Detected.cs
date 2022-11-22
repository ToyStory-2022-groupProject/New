using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    public GameObject pointlight;
    public GameObject lampTOmirror;
    public GameObject mirrorLight;
    public GameObject bookParticle;
    public GameObject Key;
    
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detect();
    }
    void detect()
    {
        Debug.DrawRay(transform.position, transform.up * 20f, Color.magenta);
        if(Physics.Raycast(transform.position, transform.up, 20f, LayerMask.GetMask("Mirror")))
        {
            timer += Time.deltaTime;
            mirrorLight.SetActive(true);
            pointlight.SetActive(true);
            lampTOmirror.SetActive(true);
            Key.SetActive(true);
            /*if(timer > 2f)
                bookParticle.SetActive(true);*/
        }   
        else if(Physics.Raycast(transform.position, transform.up, 20f, LayerMask.GetMask("Puzzle")))
        {
            timer = 0.0f;
            pointlight.SetActive(true);
            lampTOmirror.SetActive(true);
            mirrorLight.SetActive(false);
            Key.SetActive(false);
            //bookParticle.SetActive(false);
        }
        else
        {
            timer = 0.0f;
            pointlight.SetActive(false);
            lampTOmirror.SetActive(false);
            mirrorLight.SetActive(false);
            Key.SetActive(false);
            //bookParticle.SetActive(false);
        }
    }
}
