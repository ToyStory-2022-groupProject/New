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

    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
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
            StartCoroutine(Dead());
        }
    
        if (stage1Cam.activeSelf == false)
        {
            gameObject.SetActive(false);
            crackerClear.StopSound();
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

    IEnumerator Dead()
    {
        dead = false;
        end = true;
        nav.enabled = false;
        anim.enabled = false;
        audioSource.Play();
        GameOver.Restart(0.1f, 0.1f);
        yield return null;
        isWitchMove = true;
        end = false;
        gameObject.transform.position = initPos;
        nav.enabled = true;
        anim.enabled = true;
    }
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
