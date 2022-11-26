using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage3Manager : MonoBehaviour
{
    public Rigidbody doorRigid3;
    public Rigidbody doorRigid4;
    public DataManager DataManager;
    public CheckPointer CheckPointer;
    public Cat Cat;
    public Safe Safe;
    public BatteryCatch BatteryCatch;
    

    void Start()
    {
        Safe = FindObjectOfType<Safe>();
        BatteryCatch = FindObjectOfType<BatteryCatch>();
        Cat = FindObjectOfType<Cat>();

        DataManager.Checking();
        if(DataManager.dataExist)
        {
            DataManager.Load();
            
            if(DataManager.PointNum >= 5)
            {
                doorRigid3.isKinematic = false;
            }
            if(DataManager.PointNum == 6)
            {
                Safe.SafeOpen();
            }
            if(DataManager.PointNum == 7)
            {
                doorRigid4.isKinematic = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Safe.isSafePuzzleClear == false && BatteryCatch.isStop == true)
            CheckPointer.checking[6] = true;
    }

}
