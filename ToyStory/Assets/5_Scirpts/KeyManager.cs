using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction {LEFT, RIGHT, WALK, JUMP, KeyCount}
public static class KeySetting {public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();}
public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightShift, KeyCode.Space};
    int key;
    void Awake()
    {
        for(int i = 0; i<(int)KeyAction.KeyCount; i++)
            KeySetting.keys.Add((KeyAction) i, defaultKeys[i]);
    }

   
}
