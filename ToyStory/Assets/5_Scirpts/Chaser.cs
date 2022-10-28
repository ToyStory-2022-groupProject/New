using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Chaser : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject player;
    public CheckSight CheckSight;
    public BoxCollider boxCollider;
    public float monkeySpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckSight.isDetected)
        {
            nav.speed = monkeySpeed;
            boxCollider.enabled = false;
        }
        else
        {
            nav.speed = 0f;
            boxCollider.enabled = true;
        }
        nav.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && nav.speed != 0f) 
        {
            Debug.Log("게임오버!!!!");
        }
    }
}
