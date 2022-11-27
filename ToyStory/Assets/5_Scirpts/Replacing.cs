using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replacing : MonoBehaviour
{
    public GameObject[] tPiece;
    public bool[] replacePiece;
    public GameObject key;
    public PlayerController PlayerController;
    Vector3 kLocation;
    Vector3 kRotation;

    public GameObject Monkey;
    Vector3 mLocation;
    Vector3 mRotation;

    public CPointData CPointData;
    public TrainPiece TrainPiece;
    public TrainLighting TrainLighting;
    private float timer;
    public DataManager tdata;
    public CheckPointer check4;
    
    void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        TrainLighting = FindObjectOfType<TrainLighting>();
        replacePiece = new bool[tPiece.Length];

        kLocation = key.transform.position;
        kRotation = key.transform.eulerAngles;

        mLocation = Monkey.transform.position;
        mRotation = Monkey.transform.eulerAngles;

        tdata.Checking();
        
        if(tdata.dataExist)
        {
            tdata.Load();
            for(int i = 0; i < tPiece.Length; i++)
                tPiece[i].GetComponent<TrainPiece>().done = tdata.trainPuzzle[i];
            Replace();
        }

    }

    // Update is called once per frame.
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("리플레이스타이머 " + timer);
        if(TrainLighting.Connected == true && TrainLighting.isOperate == false)
        {
            kLocation = key.transform.position;
            kRotation = key.transform.eulerAngles;
        }

        if(replacePiece[0] == true && replacePiece[1] == true && replacePiece[2] == true && replacePiece[3] == true)
            check4.checking[4] = true;
    }
    
    public void activedTrain()
    {      
        for(int i = 0; i < tPiece.Length; i++)
        {
            if(tPiece[i].GetComponent<TrainPiece>().done)
                replacePiece[i] = true;
            else
                replacePiece[i] = false;
        }
    }

    public IEnumerator Replace()
    {
        PlayerController.isGrab = false;
        activedTrain();

        for(int i = 0; i < tPiece.Length; i++)
        {
            
            if(replacePiece[i] == false)
            {
                CPointData.saveObject[i].transform.eulerAngles = CPointData.rotation[i];
                CPointData.saveObject[i].transform.position = CPointData.location[i];
            }
            Debug.Log(CPointData.saveObject.Length);
            
            if(replacePiece[i] == true)
            {
                tPiece[i].GetComponent<TrainPiece>().original.SetActive(true);
                tPiece[i].GetComponent<TrainPiece>().piece.SetActive(false);
            }
        }

        Monkey.transform.position = mLocation;
        Monkey.transform.eulerAngles = mRotation;

        key.transform.position = kLocation;
        key.transform.eulerAngles = kRotation;
        
        yield return YieldInstructionCache.WaitForSeconds(1f);

        Monkey.GetComponent<Chaser>().stopDetect = false;
    }
    
    // public void Replace()
    // {
    //     PlayerController.isGrab = false;
    //     activedTrain();
    //
    //     for(int i = 0; i < tPiece.Length; i++)
    //     {
    //         
    //         if(replacePiece[i] == false)
    //         {
    //             CPointData.saveObject[i].transform.eulerAngles = CPointData.rotation[i];
    //             CPointData.saveObject[i].transform.position = CPointData.location[i];
    //         }
    //         Debug.Log(CPointData.saveObject.Length);
    //         
    //         if(replacePiece[i] == true)
    //         {
    //             tPiece[i].GetComponent<TrainPiece>().original.SetActive(true);
    //             tPiece[i].GetComponent<TrainPiece>().piece.SetActive(false);
    //         }
    //     }
    //
    //     Monkey.transform.position = mLocation;
    //     Monkey.transform.eulerAngles = mRotation;
    //     
    //     // if(timer > 10f)
    //     // {
    //     //     Monkey.GetComponent<Chaser>().stopDetect = false;
    //     //     timer = 0;
    //     // }
    //
    //     key.transform.position = kLocation;
    //     key.transform.eulerAngles = kRotation;
    //     
    //     Monkey.GetComponent<Chaser>().stopDetect = false;
    // }
}
