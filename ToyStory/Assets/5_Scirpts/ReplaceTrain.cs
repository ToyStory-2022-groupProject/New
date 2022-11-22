using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceTrain : MonoBehaviour
{
    public GameObject[] trainPuzzle;
    public bool[] replacePiece;
    public CPointData CPointData;
    public TrainPiece TrainPiece;

    void Start()
    {
        CPointData = GetComponent<CPointData>();
        TrainPiece = GetComponent<TrainPiece>();
        replacePiece = new bool [trainPuzzle.Length];
    }

    // Update is called once per frame.
    void Update()
    {
        
    }
    
    void activedTrain()
    {
        for(int i = 0; i < trainPuzzle.Length; i++)
        {
            if(trainPuzzle[i].done = true)
                replacePiece[i] = true;
            else
                replacePiece[i] = false;
        }
    }
    
    public void Replace()
    {
        activedTrain();

        for(int i = 0; i < trainPuzzle.Length; i++)
        {
            if(replacePiece[i] == false)
            {
                CPointData.saveObject[i].transform.eulerAngles = CPointData.rotation[i];
                CPointData.saveObject[i].transform.position = CPointData.location[i];
            }
            if(replacePiece[i] == true)
            {
                TrainPiece.original.SetActive(true);
                TrainPiece.piece.SetActive(false);
            }
        }
    }
}
