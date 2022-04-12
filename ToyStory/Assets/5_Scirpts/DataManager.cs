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
    public static DataManager instance;
    public CheckPointer checkPointer; //체크포인트 확인
    private int StageNum; //스테이지 번호 확인
    string jsonData; //저장하고 불러올 데이터
    string path;
    string filename = "saveData"; //파일명 지정
    // Start is called before the first frame update
    void Awake()
    {
        #region singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance.gameObject);
        DontDestroyOnLoad(this.gameObject);
        #endregion
    
        path = Application.persistentDataPath + "/"; //Unity에서 지원하는 파일 경로
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            Save();
        else if(Input.GetKeyDown(KeyCode.F2))
            Load();
        else if(Input.GetKeyDown(KeyCode.R))
            resetData();
    }
    
    public void resetData() //데이터 초기화
    {
        System.IO.File.Delete(path + filename);
    }
    private void getData() //저장할 데이터 받아오기
    {
        checkPointer.FindCheckPoint();
        StageNum = SceneManager.GetActiveScene().buildIndex;
    }
    public void Save()
    {
        getData();
        Data Save = new Data() {Stage = StageNum, Checkpoint = checkPointer.pointNum};
        jsonData = JsonUtility.ToJson(Save);
        File.WriteAllText(path + filename, jsonData);
        Debug.Log(jsonData);
    }


    public bool dataExist; //json파일이 존재하는지 확인
    public void Load()
    {
        if(System.IO.File.Exists(path + filename))
        {
            dataExist = true;
            jsonData = File.ReadAllText(path + filename);
            Data Load = JsonUtility.FromJson<Data>(jsonData);
            Debug.Log("파일 존재");
            Debug.Log(Load);
        }
        else
        {
            dataExist = false;
            Debug.Log("저장된 파일이 없습니다.");
        }
    }
}
