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
    public bool puzzleClear;

    void Update()
    {
        if(puzzleClear)
        {
            check = true;
            Candy.SetActive(false);
            answerCandy.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == Candy.name)
            puzzleClear = true;
    }
}
