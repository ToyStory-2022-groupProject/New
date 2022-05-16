using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameObject Player;
    public CheckPointer checkPointer;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Restart()
    {
        Debug.Log("Restart");
        checkPointer.FindCheckPoint();
        Player.transform.position = checkPointer.checkPoint[checkPointer.pointNum].transform.position;
    }
}
