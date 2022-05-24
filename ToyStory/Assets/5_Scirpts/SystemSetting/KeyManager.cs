using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction {LEFT, RIGHT, UP, Down, WALK, JUMP, GRAB, CAMUP, CAMDOWN, KeyCount}
public static class KeySetting {public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();}
public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightShift, KeyCode.Space, KeyCode.LeftControl, KeyCode.W, KeyCode.S};
    static KeyManager Instance;
    KeyCode keyLeft, keyRight, keyUp, keyDown;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadKey();    
        keyLeft = KeySetting.keys[KeyAction.LEFT];
        keyRight = KeySetting.keys[KeyAction.RIGHT];
        keyUp = KeySetting.keys[KeyAction.UP];
        keyDown = KeySetting.keys[KeyAction.Down];
    }

    public void LoadKey()
    {
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

    public void TopViewKey()
    {
        KeySetting.keys[KeyAction.LEFT] = keyDown;
        KeySetting.keys[KeyAction.RIGHT] = keyUp;
        KeySetting.keys[KeyAction.UP] = keyLeft;
        KeySetting.keys[KeyAction.Down] = keyRight;
    }

    public void NormalKey()
    {
        KeySetting.keys[KeyAction.LEFT] = keyLeft;
        KeySetting.keys[KeyAction.RIGHT] = keyRight;
        KeySetting.keys[KeyAction.UP] = keyUp;
        KeySetting.keys[KeyAction.Down] = keyDown;
    }
}
