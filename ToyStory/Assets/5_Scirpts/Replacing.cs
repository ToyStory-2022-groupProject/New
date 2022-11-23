using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replacing : MonoBehaviour
{
    public GameObject[] tPiece;
    public bool[] replacePiece;
    public CPointData CPointData;
    public TrainPiece TrainPiece;

    private float timer;
    
    void Start()
    {
        TrainPiece = GetComponent<TrainPiece>();
        replacePiece = new bool [tPiece.Length];
    }

    // Update is called once per frame.
    void Update()
    {
        timer += Time.deltaTime;
    }
    
    void activedTrain()
    {
        for(int i = 0; i < tPiece.Length; i++)
        {
            if(tPiece[i].GetComponent<TrainPiece>().done)
                replacePiece[i] = true;
            else
                replacePiece[i] = false;
        }
    }
    
    public void Replace()
    {
        activedTrain();

        for(int i = 0; i < tPiece.Length; i++)
        {
            if(replacePiece[i] == false)
            {
                CPointData.saveObject[i].transform.eulerAngles = CPointData.rotation[i];
                CPointData.saveObject[i].transform.position = CPointData.location[i];
            }
            if(replacePiece[i] == true)
            {
                tPiece[i].GetComponent<TrainPiece>().original.SetActive(true);
                tPiece[i].GetComponent<TrainPiece>().piece.SetActive(false);
            }
        }

        CPointData.saveObject[tPiece.Length].transform.eulerAngles = CPointData.rotation[tPiece.Length];
        CPointData.saveObject[tPiece.Length].transform.position = CPointData.location[tPiece.Length];
        
        if(timer > 10f)
        {
            CPointData.saveObject[tPiece.Length].GetComponent<Chaser>().stopDetect = false;
            timer = 0;
        }
    }
}
