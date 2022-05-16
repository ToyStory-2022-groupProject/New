using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class KeySettingUI : MonoBehaviour
{
    public Text[] KeyName;
    
    void Start()
    {
        for(int i = 0; i < KeyName.Length; i++) //ui에 현재 키설정 표시
        {
            KeyName[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < KeyName.Length; i++) //ui에 현재 키설정 표시
        {
            KeyName[i].text = KeySetting.keys[(KeyAction)i].ToString();
        }
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if(key >= 0)
        {
            if(keyEvent.isKey)
            {
                KeyCode current = keyEvent.keyCode;
                if(Input.GetKey(KeyCode.Backspace)) //백스페이스 누를 시 키 삭제
                {
                    KeySetting.keys[(KeyAction) key] = KeyCode.None;
                    PlayerPrefs.SetInt(key.ToString(), (int)KeyCode.None);
                    key = -1;
                }
                    
                else if(!KeySetting.keys.ContainsValue(current)) //같은 값 없을 시 추가
                {
                    KeySetting.keys[(KeyAction) key] = current;
                    PlayerPrefs.SetInt(key.ToString(), (int)current);
                    Debug.Log((int)current);
                    key = -1;
                }
                    
                else if(KeySetting.keys.ContainsValue(current)) //같은 값 있을 시 기존 값 삭제 후 추가
                {
                    KeyAction exist = KeySetting.keys.FirstOrDefault(x => x.Value == current).Key;
                    KeySetting.keys[(KeyAction) key] = current;
                    PlayerPrefs.SetInt(key.ToString(), (int)current);  
                    key = -1;
                    KeySetting.keys[exist] = KeyCode.None;
                }
            }   
        }   
    }

    int key = -1;
    public void BtnNum(int num)
    {
        key = num;
    }

}
