using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    // 이제 안씀 삭제해도 무방
    [SerializeField] Transform player;
    [SerializeField] float speed; // 카메라 시점 회전 속도 w/s
    
    static public Vector3 offset;
    static public Vector3 targetRot;
    static public bool isRotate;
    
    public bool isTopView;
    public KeyManager keyManager;
    private Quaternion asd;
    float h;

    private void Start()
    {
        asd = Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
    }

    void Update()
    {
        transform.position = player.position + offset;
        if (isRotate)
        {
            Rotation();
        }
        //CameraRotate();
    }

    void Rotation()
    {
        Debug.Log("실행");
        transform.rotation = Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
    }
    
    // void CameraRotate() // 카메라 회전
    // {
    //     Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
    //     
    //     if (Input.GetKey(KeySetting.keys[KeyAction.CAMDOWN]) || Input.GetKey(KeySetting.keys[KeyAction.CAMUP]))
    //     {
    //         rot.x += -1 * speed * (Input.GetKey(KeySetting.keys[KeyAction.CAMDOWN]) ? -1 : 1);
    //         Quaternion q = Quaternion.Euler(rot); // Quaternion으로 변환
    //         //if(transform.rotation.x > -0.003f && transform.rotation.x < 0.3f) // 범위 지정
    //             transform.rotation = Quaternion.Slerp(transform.rotation, q, 0.01f); // 자연스럽게 회전
    //     }
    //     else // w키 s키 누르기 전으로 돌아가기
    //     {
    //         if(!isTopView)
    //             transform.rotation = Quaternion.Slerp(transform.rotation, sideViewRotOffset, 0.1f);
    //         else
    //             transform.rotation = Quaternion.Slerp(transform.rotation, topViewRotOffset, 0.1f);
    //     }
    // }
    
    // public void TopViewIn()
    // {
    //     Debug.Log("ti");
    //     isTopView = true;
    //     StartCoroutine("ViewTop");
    //     keyManager = FindObjectOfType<KeyManager>();
    //     keyManager.TopViewKey();
    // }
    //
    // public void TopViewOut()
    // {
    //     Debug.Log("to");
    //     isTopView = false;
    //     StartCoroutine("ViewTop");
    //     keyManager = FindObjectOfType<KeyManager>();
    //     keyManager.NormalKey();
    // }
    //
    // IEnumerator ViewTop() // 뷰 전환 코루틴
    // {
    //     player.GetComponent<PlayerController>().scriptOff();
    //     if (isTopView)
    //     {
    //         transform.position = Vector3.Slerp(transform.position, topViewOffset, 0.001f);
    //         transform.rotation = Quaternion.Lerp(sideViewRotOffset, topViewRotOffset, 0.001f);
    //     }
    //     else
    //     {
    //         transform.position = Vector3.Slerp(topViewOffset, sideViewOffset, 0.001f);
    //         transform.rotation = Quaternion.Lerp(topViewRotOffset, sideViewRotOffset, 0.001f);
    //     }
    //     yield return new WaitForSeconds(1f); 
    //     player.GetComponent<PlayerController>().enabled = true;
    // }
}
