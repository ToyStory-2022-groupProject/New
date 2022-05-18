using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Data //저장할 데이터
{
    public int Stage;
    public int Checkpoint;
}
public class DataManager : MonoBehaviour
{
    CheckPointer checkPointer; //체크포인트 확인
    public int StageNum; //스테이지 번호 확인
    public int PointNum;
    string jsonData; //저장하고 불러올 데이터
    string path;
    string filename = "saveData"; //파일명 지정
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
        StageNum = SceneManager.GetActiveScene().buildIndex;
    }
    public void Save()
    {
        getData();
        Data Save = new Data() {Stage = StageNum, Checkpoint = PointNum};
        jsonData = JsonUtility.ToJson(Save);
        File.WriteAllText(path + filename, jsonData);
        Debug.Log(jsonData);
    }

    public void Load()
    {
        jsonData = File.ReadAllText(path + filename);
        Data Load = JsonUtility.FromJson<Data>(jsonData);
        StageNum = Load.Stage;
        PointNum = Load.Checkpoint;
    }
    public bool dataExist; //json파일이 존재하는지 확인

    public void Checking()
    {
        if(System.IO.File.Exists(path + filename))
        {
            dataExist = true;
            Debug.Log("파일 존재");
        }
        else
        {
            dataExist = false;
            Debug.Log("저장된 파일이 없습니다.");
        }
    }
   
   
}
