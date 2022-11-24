using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1Manager : MonoBehaviour
{
    public GameObject[] candyPuzzles;
    public DataManager DataManager;
    public CheckPointer Checkpointer;
    public CPointData CPointData;
    public bool candyClear;
    void Start()
    {
        DataManager.Checking();
        if(DataManager.dataExist)
        {
            DataManager.Load();
            if(DataManager.c1 == true)
            {
                Debug.Log("위치 재설정");
                for (int i = 0; i < 2; i++)
                {
                    candyPuzzles[i].SetActive(true);
                    candyPuzzles[i].GetComponent<CheckingPuzzle>().puzzleClear = true;
                    CPointData.saveObject[i].transform.eulerAngles = DataManager.Rotation[i];
                    CPointData.saveObject[i].transform.position = DataManager.Location[i];
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(candyPuzzles[0].activeSelf == true && candyPuzzles[1].activeSelf == true)
        {
            Debug.Log("발판 생성");
            if(candyPuzzles[0].GetComponent<CheckingPuzzle>().puzzleClear == true && candyPuzzles[1].GetComponent<CheckingPuzzle>().puzzleClear == true)
            {
                Debug.Log("퍼즐 1클리어");
                Checkpointer.checking[1] = true;
                CPointData.saveLocation();
            }
                
        }
        
    }

}
