using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGuide : KeySettingUI
{
    static KeyGuide instance;

    public static KeyGuide Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<KeyGuide>();
                
                if (obj != null)
                    instance = obj;
                else
                    instance = Create();
            }
            return instance;
        }
    }
    
    static KeyGuide Create()
    {
        var loadPrefab = Resources.Load<KeyGuide>("KeyGuide");
        return Instantiate(loadPrefab);
    }
    
    public void LoadSubMenu()
    {
        gameObject.SetActive(true);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.isKeyGuide = false;
            Destroy(gameObject);
        }
    }
}
