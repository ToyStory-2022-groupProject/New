using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if(keyEvent.isKey)
        {
            KeySetting.keys[(KeyAction) key] = keyEvent.keyCode;
            key = -1;
        }
    }
    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
    }

}
