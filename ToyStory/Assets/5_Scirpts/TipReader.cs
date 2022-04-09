using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class TipReader : MonoBehaviour
{
    [SerializeField] Text tip;
    int ran;
    string[] tips;
    public void TipChange()
    {
        string path = Application.dataPath;
        path += "/6_Sprite/sample.txt";

        tips = File.ReadAllLines(path);
        TipChange();
        
        ran = Random.Range(0, tips.Length);
        tip.text = string.Format("Tip : " + tips[ran]);
    }
}
