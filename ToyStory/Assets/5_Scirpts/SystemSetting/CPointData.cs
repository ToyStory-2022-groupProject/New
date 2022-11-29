using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPointData : MonoBehaviour
{
    public GameObject[] saveObject;

    public List<Vector3> location = new List<Vector3>();
    public List<Vector3> rotation = new List<Vector3>();
    public int objectNum;
    // Start is called before the first frame update
    void Start()
    {
        objectNum = saveObject.Length;
        for(int i = 0; i < objectNum; i++)
        {
            location.Add(saveObject[i].transform.position);
            rotation.Add(saveObject[i].transform.eulerAngles);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initialize() //게임오버 시 해당 오브젝트 초기화
    {
        for(int i = 0; i < objectNum; i++)
        {
            saveObject[i].transform.eulerAngles = rotation[i];
            saveObject[i].transform.position = location[i];
        }
    }

    public void saveLocation() //오브젝트 위치값 저장
    {
        for(int i = 0; i < objectNum; i++)
        {
            rotation[i] = saveObject[i].transform.eulerAngles;
            location[i] = saveObject[i].transform.position;
        }
    }
}
