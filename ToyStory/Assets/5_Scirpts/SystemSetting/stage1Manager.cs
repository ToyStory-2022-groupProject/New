using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1Manager : MonoBehaviour
{
    public GameObject[] candyPuzzles;
    public DataManager DataManager;
    public CheckPointer Checkpointer;
    public CPointData CPointData0, CPointData1;
    public bool candyClear;
    void Start()
    {
        DataManager.Checking();
        if(DataManager.dataExist)
        {
            DataManager.Load();
            if(DataManager.PointNum == 0) //벤트 위치 고정
            {
                CPointData0.saveObject[0].transform.eulerAngles = DataManager.Rotation[0];
                CPointData0.saveObject[0].transform.position = DataManager.Location[0];
            }
            
            if(DataManager.PointNum >= 1)
            {
                Debug.Log("위치 재설정");
                for (int i = 0; i < 2; i++)
                {
                    candyPuzzles[i].SetActive(true);
                    candyPuzzles[i].GetComponent<CheckingPuzzle>().puzzleClear = true;
                    CPointData1.saveObject[i].transform.eulerAngles = DataManager.Rotation[i+1];
                    CPointData1.saveObject[i].transform.position = DataManager.Location[i+1];
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Checkpointer.checking[0] == true)
        {
            CPointData0.saveLocation();
        }
        if(candyPuzzles[0].activeSelf == true && candyPuzzles[1].activeSelf == true)
        {
            Debug.Log("발판 생성");
            if(candyPuzzles[0].GetComponent<CheckingPuzzle>().puzzleClear == true && candyPuzzles[1].GetComponent<CheckingPuzzle>().puzzleClear == true)
            {
                Debug.Log("퍼즐 1클리어");
                Checkpointer.checking[1] = true;
                CPointData1.saveLocation();
            }
                
        }
        
    }

}
