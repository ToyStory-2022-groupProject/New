using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Chaser : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject player;
    public CheckSight CheckSight;
    //public BoxCollider boxCollider;
    private BoxCollider boxCollider;
    public GameObject cymbalsMonkey;
    public GameOver GameOver;
    public Replacing Replacing;
    public bool stopDetect = false;
    public float monkeySpeed;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (CheckSight.isDetected && stopDetect == false)
        {
            nav.enabled = true;
            boxCollider.isTrigger = true;
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.enabled = false;
            boxCollider.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            stopDetect = true;
            player.GetComponent<PlayerController>().scriptOff();
            GameOver = FindObjectOfType<GameOver>();
            GameOver.Restart(0.1f, 0.1f); 
            // Replacing.Replace();
            StartCoroutine(Replacing.Replace());
            Debug.Log("원숭이로부터 게임오버!!!!");
        }
    }

  
}
