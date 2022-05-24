using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] Vector3 sideViewOffset;
    [SerializeField] Vector3 topViewOffset;
    [SerializeField] Quaternion sideViewRotOffset;
    [SerializeField] Quaternion topViewRotOffset;
    [SerializeField] float speed; // 카메라 시점 회전 속도 w/s
    
    public bool isTopView;
    public KeyManager keyManager;
    float h;
    void Update()
    {
        CameraRotate();
        if (!isTopView)
        {
            transform.position = player.transform.position + sideViewOffset;
        }
        else
        {
            transform.position = player.transform.position + topViewOffset;
        }

    }

    void CameraRotate() // 카메라 회전
    {
        Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
        
        if (Input.GetKey(KeySetting.keys[KeyAction.CAMDOWN]) || Input.GetKey(KeySetting.keys[KeyAction.CAMUP]))
        {
            rot.x += -1 * speed * (Input.GetKey(KeySetting.keys[KeyAction.CAMDOWN]) ? -1 : 1);
            Quaternion q = Quaternion.Euler(rot); // Quaternion으로 변환
            //if(transform.rotation.x > -0.003f && transform.rotation.x < 0.3f) // 범위 지정
                transform.rotation = Quaternion.Slerp(transform.rotation, q, 0.01f); // 자연스럽게 회전
        }
        else
        {
            if(!isTopView)
                transform.rotation = Quaternion.Slerp(transform.rotation, sideViewRotOffset, 0.1f);
            else
                transform.rotation = Quaternion.Slerp(transform.rotation, topViewRotOffset, 0.1f);
        }
    }

    public void TopViewIn()
    {
        Debug.Log("ti");
        isTopView = true;
        transform.position = Vector3.Slerp(transform.position, topViewOffset, 0.1f);
        transform.rotation = Quaternion.Lerp(sideViewRotOffset, topViewRotOffset, 0.1f);
        keyManager = FindObjectOfType<KeyManager>();
        keyManager.TopViewKey();
    }
    
    public void TopViewOut()
    {
        Debug.Log("to");
        isTopView = false;
        transform.position = Vector3.Slerp(topViewOffset, sideViewOffset, 0.1f);
        transform.rotation = Quaternion.Lerp(topViewRotOffset, sideViewRotOffset, 0.1f);
        keyManager = FindObjectOfType<KeyManager>();
        keyManager.NormalKey();
    }
}
