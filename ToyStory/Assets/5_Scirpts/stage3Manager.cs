using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage3Manager : MonoBehaviour
{
    public Rigidbody doorRigid3;
    public Rigidbody doorRigid4;
    public DataManager dataManager;
    public CheckPointer CheckPointer;
    public Cat cat;
    public Safe safe;
    public BatteryCatch BatteryCatch;
    public BoxCollider clockTrigger;

    public GameObject catObj;

    public GameObject key3;
    public GameObject key4;

    public GameObject spurnCam;
    public GameObject stage4BasicCam;
    void Start()
    {
        dataManager.Checking();
        if(dataManager.dataExist)
        {
            dataManager.Load();
            Debug.Log("asd" + DataManager.PointNum + "bsd" + TikTok.inStage);
            if(DataManager.PointNum >= 5 && TikTok.inStage)
            {
                key3.SetActive(false);
                doorRigid3.isKinematic = false;
            }
            if(DataManager.PointNum >= 7)
            {
                BatteryCatch.isStop = true;
            }
            if(DataManager.PointNum <= 7)
            {
                Cat.isCatSpurned = false;
            }

            if(DataManager.PointNum == 8 && Cat.isCatSpurned == false)
            {
                safe.SafeOpen();
            }
            if(DataManager.PointNum >= 8 && Cat.isCatSpurned == true)
            {
                safe.SafeOpen();
                doorRigid4.isKinematic = false;
                cat.checkPointer8.SetActive(true);
                catObj.SetActive(false);
                key4.SetActive(false);
                spurnCam.SetActive(false);
                stage4BasicCam.SetActive(true);
            }
        }
        
    }

}
