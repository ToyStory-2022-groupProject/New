using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class Data //저장할 데이터
{
    public int Checkpoint;
    public List<Vector3> objectLocation;
    public List<Vector3> objectRotation;
    public bool[] train = new bool[4];
    public bool stage3;
    public bool house;
    public bool clocknsafe;
    public bool shadow;
}
public class DataManager : MonoBehaviour
{
    public CheckPointer checkPointer; //체크포인트 확인
    public Replacing Replacing;
    public List<Vector3> Location = new List<Vector3>();
    public List<Vector3> Rotation = new List<Vector3>();
    public bool[] trainPuzzle;
    public bool c1, c5, c6, c7;
    public int PointNum;
    string jsonData; //저장하고 불러올 데이터
    string path;
    string filename = "Data"; //파일명 지정
    // Start is called before the first frame update
    void Awake()
    {
        checkPointer = GetComponent<CheckPointer>();
        path = Application.persistentDataPath + "/"; //Unity에서 지원하는 파일 경로
    }
    
    public void resetData() //데이터 초기화
    {
        GameManager.isF1 = false; // 새로하기 눌렀을때 가이드창 다시 표시
        System.IO.File.Delete(path + filename);
        Debug.Log("데이터 초기화");
    }
    private void getData() //저장할 데이터 받아오기
    {
        checkPointer.FindCheckPoint();
        PointNum = checkPointer.pointNum;
        for(int i = 0; i < PointNum + 1; i++)
        {
            if(i == 1)
            {
                c1 = true;
                for(int j = 0; j < checkPointer.checkPoint[i].GetComponent<CPointData>().objectNum; j++)
                {
                    Location.Add(checkPointer.checkPoint[i].GetComponent<CPointData>().location[j]);
                    Rotation.Add(checkPointer.checkPoint[i].GetComponent<CPointData>().rotation[j]);
                }     
            }
            if(i == 5)
                c5 = true;
            if(i == 6)
                c6 = true;
            if(i == 7)
                c7 = true;
        }
    }
    public void Save()
    {
        Replacing = FindObjectOfType<Replacing>();
        getData();
        Replacing.activedTrain();
        Data Save = new Data() {Checkpoint = PointNum, objectLocation = Location.ToList(), objectRotation = Rotation.ToList(), train = Replacing.replacePiece.ToArray(), stage3 = c5,  house = c1, clocknsafe = c6, shadow = c7};
        jsonData = JsonUtility.ToJson(Save);
        File.WriteAllText(path + filename, jsonData);
        Debug.Log(jsonData);
    }

    public void Load()
    {
        jsonData = File.ReadAllText(path + filename);
        Data Load = JsonUtility.FromJson<Data>(jsonData);
        PointNum = Load.Checkpoint;
        Location = Load.objectLocation.ToList();
        Rotation = Load.objectRotation.ToList();
        trainPuzzle = Load.train.ToArray();
        c1 = Load.house;
        c5 = Load.stage3;
        c6 = Load.clocknsafe;
        c7 = Load.shadow;
    }

    public bool dataExist; //json파일이 존재하는지 확인

    public void Checking()
    {
        if(System.IO.File.Exists(path + filename))
        {
            dataExist = true;
        }
        else
        {
            dataExist = false;
        }
    }
}
