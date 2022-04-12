using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointer : MonoBehaviour
{   public GameObject[] checkPoint;
    bool[] checking;

    public int pointNum = -1;

    void Start()
    {
        checking = new bool[checkPoint.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            FindCheckPoint();
    }

    public void FindCheckPoint()
    {
        if(checking[0] == false)
        {
            pointNum = 0;
        }
        else if(checking[checking.Length-1] == true)
        {
           pointNum = checking.Length-1;
        }
        else
        {
            for(int i = 1; i < checking.Length; i++)
            {
                if(checking[i] == false)
                {
                    pointNum = i-1;
                    return;
                }
            }
        }    
    }

    public void TriggerCheck(int num)
    {
        checking[num] = true;
    }

}
