using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1Manager : MonoBehaviour
{
    public GameObject[] candyPuzzles;
    public DataManager DataManager;
    public CheckPointer check1;
    public CPointData CPointData;
    public bool candyClear;
    void Start()
    {
        check1 = GetComponent<CheckPointer>();
        DataManager.Checking();
        if(DataManager.c1 == true)
        {
            for (int i = 0; i < 2; i++)
            {
                candyPuzzles[i].SetActive(true);
                candyPuzzles[i].GetComponent<CheckingPuzzle>().puzzleClear = true;
                CPointData.saveObject[i].transform.eulerAngles = DataManager.Rotation[i];
                CPointData.saveObject[i].transform.position = DataManager.Location[i];
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(candyPuzzles[0].activeSelf == true && candyPuzzles[1].activeSelf == true)
        {
            if(candyPuzzles[0].GetComponent<CheckingPuzzle>().check == true && candyPuzzles[1].GetComponent<CheckingPuzzle>().check == true)
                check1.checking[1] = true;
        }
        
    }

}
