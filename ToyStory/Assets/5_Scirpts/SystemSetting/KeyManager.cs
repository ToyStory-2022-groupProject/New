using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction {LEFT, RIGHT, UP, Down, WALK, JUMP, GRAB, CAMUP, CAMDOWN, KeyCount}
public static class KeySetting {public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();}
public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightShift, KeyCode.Space, KeyCode.LeftControl, KeyCode.W, KeyCode.S};
    static KeyManager Instance;
  
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for(int i = 0; i<(int)KeyAction.KeyCount; i++)
        {
            if(PlayerPrefs.HasKey(i.ToString()))
            {
                int key = PlayerPrefs.GetInt(i.ToString());
                KeySetting.keys.Add((KeyAction)i, (KeyCode)key);
            }
            else
                KeySetting.keys.Add((KeyAction) i, defaultKeys[i]);
        }       
    }
}
