using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingPuzzle : MonoBehaviour
{ 
    public GameObject Candy;
    public GameObject answerCandy;
    public GameObject floor;
    public bool check;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == Candy.name)
        {
            check = true;
            Candy.SetActive(false);
            answerCandy.SetActive(true);
        }
    }
}
