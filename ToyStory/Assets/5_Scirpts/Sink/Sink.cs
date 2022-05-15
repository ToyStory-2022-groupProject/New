using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class Sink : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject water;
    public float speed;
    PlayerController playerController;
    bool isSwitch;
    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isSwitch && PlayerController.isGrab)
        {
            effect.SetActive(true);
            //StartCoroutine("SinkOn");
            WaterUp();
        }
        else
        {
            effect.SetActive(false);
        }
    }
    
    void WaterUp()
    {
        if (water.transform.position.y < 7.8f)
        {
            
            water.transform.Translate(Vector3.up * Time.deltaTime * speed);
            //water.transform.position += new Vector3(0, 0.05f, 0);
            //water.transform.position += new Vector3(0, 0.05f, 0);
            if (water.transform.position.y > 7.75f)
                Floater.isfull = true;
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            isSwitch = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            isSwitch = false;
        }
    }
}
