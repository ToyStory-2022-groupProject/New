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
    public GameOver GameOver;
    private CapsuleCollider col;
    private Rigidbody rb;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    
    private bool onGround;
    GameObject Rope;
    static public bool isGrab;
    public bool Handed = false;
    private bool onRope;
    private bool inWater;
    private bool isBarrier; // 배리어 여부 확인
    private bool Ladder;
    private bool OnLadder;

    static int jumpState = Animator.StringToHash("Base Layer.Jump"); 
    static int ladderState = Animator.StringToHash("Base Layer.Climb"); 
    static int pickState = Animator.StringToHash("Base Layer.Pick");
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
        StopMove();
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        ///////////////////////////////////////////////////////잡기//////////////////////////////////////////////////////////    
        if(Input.GetKeyDown(KeySetting.keys[KeyAction.GRAB]))
        {
            if(Ladder) 
            {
                OnLadder = true;
                anim.SetBool("Ladder", Ladder);
            }
            if (currentBaseState.fullPathHash == ladderState && !anim.IsInTransition(0))
            { 
                anim.SetBool("Ladder", false); 
            }
        }
        if(Input.GetKey(KeySetting.keys[KeyAction.GRAB]) && !Ladder)
        {
            isGrab = true;
            //////////////////////////////////////////줄타기
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
            else if(onGround)
            {
                if(Handed == false)
                {
                    anim.SetBool("Pick", isGrab); 
                }
                else 
                {
                    if(currentBaseState.fullPathHash == pickState && !anim.IsInTransition(0))
                    {
                        anim.SetBool("Grab", isGrab); 
                        anim.SetBool("Pick", !isGrab);
                    }
                }
            }          
        } 
        else if(Input.GetKeyUp(KeySetting.keys[KeyAction.GRAB]))
        {
            if(onRope)
            {
                isGrab = onRope = false;
                anim.SetBool("Hang", isGrab);
                Rope.transform.DetachChildren();
                rb.isKinematic = onRope;
            }
            else
            {
                isGrab = Ladder = OnLadder = false;
                anim.SetBool("Pick", isGrab);
                anim.SetBool("Grab", isGrab);
                anim.SetBool("Ladder", Ladder);
            }       
        }
        
        /////////////////////////////////////////////////////////좌우이동//////////////////////////////////////////////////////////////////
        if(Input.GetKey(KeySetting.keys[KeyAction.LEFT]) && !onRope && !OnLadder && currentBaseState.fullPathHash != pickState)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            if(inWater) //수영
            {
                if(!isBarrier)
                    transform.Translate(new Vector3(0,0,speed * 0.3f));
                anim.SetBool("Move", true);
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Swim);
            }
            else if(!inWater)
            {
                if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    if(!isBarrier)
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
                    if(!isBarrier)
                        transform.Translate(new Vector3(0,0,speed));
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                        anim.SetBool("Move", true); 
                    }                 
                }
            }

            
        }
        else if(Input.GetKey(KeySetting.keys[KeyAction.RIGHT]) && !onRope && !OnLadder && currentBaseState.fullPathHash != pickState)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            if(inWater) //수영
            {
                if(!isBarrier)
                    transform.Translate(new Vector3(0,0,speed * 0.3f));
                anim.SetBool("Move", true);
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Swim);
            }
            else if(!inWater)
            {
                if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    if(!isBarrier)
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
                    if(!isBarrier)
                        transform.Translate(new Vector3(0,0,speed)); 
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                        anim.SetBool("Move", true); 
                    }                 
                }
            }
        }
        else if(Input.GetKey(KeySetting.keys[KeyAction.UP]) && !onRope && !OnLadder && currentBaseState.fullPathHash != pickState)
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
            if(inWater) //수영
            {
                if(!isBarrier)
                    transform.Translate(new Vector3(0,0,speed * 0.3f));
                anim.SetBool("Move", true);
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Swim);
            }
            else if(!inWater)
            {
                if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    if(!isBarrier)
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
                    if(!isBarrier)
                        transform.Translate(new Vector3(0,0,speed)); 
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                        anim.SetBool("Move", true); 
                    }                 
                }
            }
        }
        else if(Input.GetKey(KeySetting.keys[KeyAction.Down]) && !onRope && !OnLadder && currentBaseState.fullPathHash != pickState)
        {
            transform.rotation = Quaternion.Euler(0,90,0);
            if(inWater) //수영
            {
                if(!isBarrier)
                    transform.Translate(new Vector3(0,0,speed * 0.3f));
                anim.SetBool("Move", true);
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Swim);
            }
            else if(!inWater)
            {
                if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    if(!isBarrier)
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
                    if(!isBarrier)
                        transform.Translate(new Vector3(0,0,speed)); 
                    if(onGround)
                    {
                        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                        anim.SetBool("Move", true); 
                    }                 
                }
            }
        }
        else if(Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT]) || Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT]) || Input.GetKeyUp(KeySetting.keys[KeyAction.UP]) || Input.GetKeyUp(KeySetting.keys[KeyAction.Down]))
        {
            SFXMgr.Instance.Stop_SFX();
            anim.SetBool("Move", false);
        }
        
             
        //////////////////////////////////////////////////////////점프///////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.JUMP]) && !inWater) // 점프키를 누르면
        {
            if(onGround)
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
    
    void StopMove() // 범위 외로 이동하려고 하는 경우
    {
        Debug.DrawRay(transform.position, transform.forward * 0.6f, Color.magenta);
        isBarrier = Physics.Raycast(transform.position, transform.forward, 0.6f, 
        LayerMask.GetMask("Barrier", "Wall", "UnPassPuzzleTool"));
    }
    
    /////////////////////////////////////////////////////////충돌 및 트리거//////////////////////////////////////////////////////////
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            inWater = false;
            anim.SetBool("InWater", inWater);
        }
        if(collision.gameObject.CompareTag("Falling"))
        {
            scriptOff();
            GameOver = FindObjectOfType<GameOver>();
            GameOver.Restart(0.1f, 0.1f);
            
        }
    }
     private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            Ladder = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ladder"))
        {
            Ladder = false;
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
            rb.isKinematic = true;
            onRope = true;
            SFXMgr.Instance.Stop_SFX();
        }
        if(point.tag == "Water")
        {
            inWater = true;
            anim.SetBool("InWater", inWater);
        }
    }

    void OnTriggerExit(Collider point)
    {
        if(point.tag == "Rope")
        {
            if(Input.GetKey(KeySetting.keys[KeyAction.JUMP]))
            {
                transform.Translate(new Vector3(0,0,speed));
            }
        }
        /*if(point.tag == "Water") 
        {
            inWater = false;
            anim.SetBool("InWater", inWater);
            Debug.Log(inWater);
        }*/
    }

    public void scriptOff()
    {
        this.GetComponent<PlayerController>().enabled = false;
        SFXMgr.Instance.Stop_SFX();
    }
}
