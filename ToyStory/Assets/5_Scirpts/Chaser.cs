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
    void Update()
    {
        if (CheckSight.isDetected)
        {
            nav.enabled = true;
            boxCollider.enabled = false;
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.enabled = false;
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("원숭이로부터 게임오버!!!!");
        }
    }
}
