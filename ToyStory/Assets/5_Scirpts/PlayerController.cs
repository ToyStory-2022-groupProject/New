using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float walkSpeed = 0;
    [SerializeField] float runSpeed = 1.0f;
    [SerializeField] float jumpPower = 1.0f;
    public CheckPointer CheckPointer;
    private CapsuleCollider col;
    private Rigidbody rb;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    
    private bool onGround;
    GameObject Rope; 
    private bool isGrab;
    private bool onRope;
    private bool inWater;
    bool left, right;
    
    static int jumpState = Animator.StringToHash("Base Layer.Jump"); 

    //시작위치 결정요소

    public DataManager dataManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        Rope = GameObject.FindGameObjectWithTag("Rope");

        dataManager.Checking();
        Set();      
    }

    void Set()
    {
        if(dataManager.dataExist)
        {
            dataManager.Load();
            Debug.Log(dataManager.PointNum);
            if(dataManager.PointNum != -1)
            {
                transform.position = CheckPointer.checkPoint[dataManager.PointNum].transform.position;
                for (int i = 0; i < dataManager.PointNum; i++)
                {
                    CheckPointer.checking[i] = true;
                }
            }     
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if(Input.GetKey(KeySetting.keys[KeyAction.GRAB]))
        {
            isGrab = true;
            if(Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
                {
                    left = true;
                }
            else if(Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
                {
                    right = true;
                }

            //////////줄타기
            if(onRope && !onGround)
            { 
                anim.SetBool("Hang",isGrab);
                transform.SetParent(Rope.transform);
                transform.localPosition = new Vector3(0,-1,0);
                if(Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
                {
                    Rope.GetComponent<Rigidbody>().AddForce(Vector3.back*jumpPower, ForceMode.Acceleration);
                }
                else if(Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
                {
                    Rope.GetComponent<Rigidbody>().AddForce(Vector3.forward*jumpPower, ForceMode.Acceleration);
                }
            }       

        }
        else if(Input.GetKeyUp(KeySetting.keys[KeyAction.GRAB]))
        {
            isGrab = onRope = left = right = false;
            anim.SetBool("Hang", isGrab);
            rb.isKinematic = false;
            Rope.transform.DetachChildren();
        }
    
        /////좌우이동
        if(Input.GetKey(KeySetting.keys[KeyAction.LEFT]) && !onRope)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            if(inWater) //수영
            {
                transform.Translate(new Vector3(0,0,speed * 0.3f));
                anim.SetBool("Move", true);
            }
            else if(!inWater)
            {
                if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    transform.Translate(new Vector3(0,0,speed * 0.3f));
                    anim.SetFloat("Speed", walkSpeed);
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Walk);
                        anim.SetBool("Move", true);
                    }
                }
                else
                {
                    anim.SetFloat("Speed", runSpeed);
                    transform.Translate(new Vector3(0,0,speed)); 
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                        anim.SetBool("Move", true); 
                    }                 
                }
            }

            
        }
        else if(Input.GetKey(KeySetting.keys[KeyAction.RIGHT]) && !onRope)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            if(inWater) //수영
            {
                transform.Translate(new Vector3(0,0,speed * 0.3f));
                anim.SetBool("Move", true);
            }
            else if(!inWater)
            {
                if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    transform.Translate(new Vector3(0,0,speed * 0.3f));
                    anim.SetFloat("Speed", walkSpeed);
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Walk);
                        anim.SetBool("Move", true);
                    }
                }
                else
                {
                    anim.SetFloat("Speed", runSpeed);
                    transform.Translate(new Vector3(0,0,speed)); 
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                        anim.SetBool("Move", true); 
                    }                 
                }
            }
        }
        else if(Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT]) || Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT]))
        {
            SFXMgr.Instance.Stop_SFX();
            anim.SetBool("Move", false);
        }
             
        //////점프
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.JUMP])) // 점프키를 누르면
        {
            if(onRope)
            {
                transform.Translate(new Vector3(0,0,speed));
            }
            if(onGround || inWater)
            {
                onGround = false;
                SFXMgr.Instance.Stop_SFX();
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetBool("Jump", true); // 점프     
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Jump);
            }
        } 
        if (currentBaseState.fullPathHash == jumpState && !anim.IsInTransition(0)) // 점프 중인 경우
        {
            SFXMgr.Instance.Stop_SFX();
            anim.SetBool("Jump", false); // 이미 점프를 수행 중이므로 이제 FALSE로 설정
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter(Collider point)
    {
        if(point.tag == "CheckPoint")
        {
            for (int i = 0; i < CheckPointer.checkPoint.Length; i++)
            {
                if (CheckPointer.checkPoint[i].gameObject == point.gameObject)
                {
                    CheckPointer.TriggerCheck(i);
                }       
            }
        }
        if(point.tag == "Rope" && isGrab)
        {
            Debug.Log("로프접촉");
            rb.isKinematic = true;
            onRope = true;
        }

        if(point.tag == "Water")
        {
            inWater = true;
            anim.SetBool("InWater", inWater);
        }
    }

    void OnTriggerExit(Collider point)
    {
        if(point.tag == "Water")
        {
            inWater = false;
            anim.SetBool("InWater", inWater);
        }
    }
}
