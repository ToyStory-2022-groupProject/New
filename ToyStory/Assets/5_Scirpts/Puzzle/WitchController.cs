using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent nav;
    public bool isWitchMove;
    public Vector3 targetPos;
    public Vector3 originPos;
    private Vector3 dest;
    private bool isNextAction;
    private int cnt;
    public GameObject stage1Cam;
    public static bool dead;
    private bool end;
    public Rigidbody playerRigid;
    public AudioSource audioSource;
    public CrackerClear crackerClear;
    public GameOver GameOver;
    public DataManager DataManager;

    float timer = 0.0f;

    private void Start()
    {
        GameOver = FindObjectOfType<GameOver>();
        dest = targetPos;
        isNextAction = true;
        isWitchMove = true;
        DataManager.Checking();
        if(DataManager.dataExist)
            DataManager.Load();
    }

    void Update()
    {
        if(DataManager.PointNum < 2) //스테이지 1 탈출하면 마녀 안 나오게
        {   
            if (isWitchMove && end == false)
            {
                isWitchMove = false;
                MoveWitch();
            }
            if(gameObject.activeSelf == false)
            {
                Debug.Log("??ASD0");
            }
            if (dead)
            {
                timer += Time.deltaTime;
                end = true;
                nav.enabled = false;
                anim.enabled = false;
                audioSource.Play();
                GameOver.Restart(0.1f, 0.1f);
                if(timer > 1)
                {
                    isWitchMove = true;
                    end = false;
                    gameObject.transform.position = originPos;
                    nav.enabled = true;
                    anim.enabled = true;
                    timer = 0.0f;
                }
                dead = false;
                Debug.Log("마녀한테 죽음");
            }
        
            if (stage1Cam.activeSelf == false)
            {
                gameObject.SetActive(false);
                crackerClear.StopSound();
            }
        }
    }

    void MoveWitch()
    {
        if (isNextAction)
        {
            nav.SetDestination(targetPos);
        }
        else
        {
            nav.SetDestination(originPos);
        }
    }
    
    // IEnumerator MoveWitch()
    // {
    //     Debug.Log("코루틴 하긴 하지??");
    //     if (isNextAction)
    //     {
    //         nav.SetDestination(targetPos);
    //     }
    //     else
    //     {
    //         nav.SetDestination(originPos);
    //     }
    //     //anim.SetBool("Walk", true);
    //     Debug.Log("isNextAction 하긴 하지??");
    //     yield return YieldInstructionCache.WaitForSeconds(10f);
    //     if (end)
    //     {
    //         StopCoroutine(MoveWitch());
    //     }
    //     else
    //     {
    //         isWitchMove = true;
    //         isNextAction = !isNextAction;
    //         Debug.Log(isNextAction);
    //     }
    //     
    //     // if (isNextAction)
    //     // {
    //     //     dest = originPos;
    //     // }
    //     // else
    //     // {
    //     //     dest = targetPos;
    //     // }
    //     // isNextAction = !isNextAction;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WitchRotate"))
        {
            if (isNextAction)
            {
                isNextAction = false;
            }
            else
            {
                isNextAction = true;
            }
            isWitchMove = true;

        }
    }
}
