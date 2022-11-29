using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointer : MonoBehaviour
{   
    public GameObject[] checkPoint;
    public bool[] checking;
    public int pointNum;

    private DataManager dataManager;
    void Start()
    {
        checking = new bool[checkPoint.Length];
        dataManager = GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("데이터매니저값: " + DataManager.PointNum);

        if(Input.GetKeyDown(KeyCode.R))
            FindCheckPoint();
    }

    public void FindCheckPoint()
    {
        if(checking[0] == false)
        {
            pointNum = -1;
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
        if(checking[num] == false)
        {
            DataManager.PointNum++;
        }
        checking[num] = true;
    }

    public void pointitialize()
    {
        FindCheckPoint();
        checkPoint[pointNum + 1].GetComponent<CPointData>().initialize();
    }
}
