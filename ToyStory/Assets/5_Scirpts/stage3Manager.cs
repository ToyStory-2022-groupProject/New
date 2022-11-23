using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage3Manager : MonoBehaviour
{
    public Rigidbody doorRigid3;
    public Rigidbody doorRigid4;
    public DataManager DataManager;
    public CheckPointer check;
    public Cat Cat;
    public Safe Safe;
    public BatteryCatch BatteryCatch;

    void Start()
    {
        check = GetComponent<CheckPointer>();
        Safe = FindObjectOfType<Safe>();
        BatteryCatch = FindObjectOfType<BatteryCatch>();
        Cat = FindObjectOfType<Cat>();

        DataManager.Checking();
        if(DataManager.dataExist)
        {
            DataManager.Load();
            
            if(DataManager.c5 == true)
            {
                doorRigid3.isKinematic = false;
            }
            if(DataManager.c7 == true)
            {
                doorRigid4.isKinematic = false;
            }
            else if(DataManager.c6 == true)
            {
                Safe.SafeOpen();
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Safe.safeClear == true && BatteryCatch.clockClear == true)
            check.checking[6] = true;
    }

}
