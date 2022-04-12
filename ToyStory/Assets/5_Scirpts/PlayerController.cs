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
    static int jumpState = Animator.StringToHash("Base Layer.Jump"); 
    
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        
        /////좌우이동
        if(onGround && Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
        {
              if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
            {
                anim.SetFloat("Speed", walkSpeed);
                transform.Translate(new Vector3(0,0,speed));
            }
            else
            {
                anim.SetFloat("Speed", runSpeed);
                transform.Translate(new Vector3(0,0,speed));
            }
            transform.rotation = Quaternion.Euler(0,180,0);
            anim.SetBool("Move", true);
        }
        else if(onGround && Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
        {
            if(Input.GetKey(KeySetting.keys[KeyAction.WALK]))
            {
                anim.SetFloat("Speed", walkSpeed);
                transform.Translate(new Vector3(0,0,speed));
            }
            else
            {
                anim.SetFloat("Speed", runSpeed);
                transform.Translate(new Vector3(0,0,speed));
            }
            transform.rotation = Quaternion.Euler(0,0,0);
            anim.SetBool("Move", true);  
        }
        else if(Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT])||Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT]))
            anim.SetBool("Move", false);

        //////점프
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.JUMP]) && onGround) // 점프키를 누르면
        {
            if (!anim.IsInTransition(0) && col) // 현재 트랜지션이 수행 중이지 않다면
            {
                onGround = false;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetBool("Jump", true); // 점프
            }
        } 
        if (currentBaseState.fullPathHash == jumpState && !anim.IsInTransition(0)) // 점프 중인 경우
        {
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
    }
}
