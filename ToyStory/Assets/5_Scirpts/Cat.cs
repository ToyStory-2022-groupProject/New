using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class Cat : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject player;
    public GameObject head; // 플레이어 위치카메라
    public float radius; // 캐릭터와 고양이 사이 거리
    private bool isChaser;
    public GameObject sleep; // 자는 애니메이션
    public GameObject run; // 달리기 애니메이션
    public GameObject attack; // 공격 애니메이션
    public GameObject stretch; // 스트레칭 애니메이션
    public CinemachineVirtualCamera stage3Cam;
    public bool isfound; // 게이지 꽉참 or 비밀번호 잠금 해제시 
    public float speed;
    public Animator anim;
    public PlayerController playerController;
    public AudioSource audioSource;
    public AudioClip[] audioClips; // 0 = 일어나는 소리 1 = 공격소리 2 = 도망갈 때 소리
    private RaycastHit[] raycastHits;
    private bool isAudioPlayed;
    
    // 고양이 내쫓기 관련
    public GameObject stage4;
    public GameObject initCam;
    public GameObject zoomIn; // 그림자 카메라 줌 인
    public GameObject zoomOut; // 카메라 줌 아웃
    public GameObject basicCam; // 카메라 줌 아웃
    public CheckSight checkSight;
    private bool isSpurn;
    private bool isInit;
    private bool isSecond;
    private bool isThird;
    private Vector3 originPosition;
    public float spurnSpeed;
    
    void Start()
    {
        isChaser = true;
        nav = GetComponent<NavMeshAgent>();
        originPosition = gameObject.transform.position;
    }

    private void Update()
    {
        FindPlayer();
        for (int i = 0; i < raycastHits.Length; i++)
        {
            RaycastHit hit = raycastHits[i];
            if (hit.collider.name == "Player")
            {
                nav.enabled = false;
                if (isAudioPlayed == false)
                {
                    isAudioPlayed = true;
                    audioSource.clip = audioClips[1];
                    audioSource.Play();
                }
                playerController.enabled = false;
                run.SetActive(false);
                attack.SetActive(true);
            }
        }
        
        if(nav.enabled && isChaser)
        {
            nav.SetDestination(player.transform.position);
        }

        if (isfound)
        {
            isfound = false;
            SFXMgr.Instance.Stop_SFX();
            anim.SetBool("Move", false);
            anim.SetBool("Jump", false);
            playerController.enabled = false;
            StartCoroutine(CatMove());
        }

        if (isSpurn)
        {
            isSpurn = false;
            StartCoroutine(CatSpurn());
        }
    }

    void FindPlayer() // 범위 외로 이동하려고 하는 경우
    {
        raycastHits = Physics.SphereCastAll(transform.position, radius, transform.position);
    }

    IEnumerator CatMove()
    {
        stage3Cam.Follow = gameObject.transform;
        sleep.SetActive(false);
        audioSource.clip = audioClips[0];
        audioSource.Play();
        stretch.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(3f);
        stretch.SetActive(false);
        run.SetActive(true);
        nav.enabled = true;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        stage3Cam.Follow = head.transform;
        playerController.enabled = true;
        playerController.Switch = false;
        anim.SetBool("Switch", false);
    }

    IEnumerator CatSpurn()
    {
        if (isInit == false)
        {
            isInit = true;
            playerController.enabled = false; 
            nav.speed = 0; 
            initCam.SetActive(false); 
            zoomIn.SetActive(true);
        }
        yield return YieldInstructionCache.WaitForSeconds(2f);
        if (isSecond == false)
        {
            isSecond = true;
            zoomIn.SetActive(false);
            zoomOut.SetActive(true);
            nav.speed = spurnSpeed;
            audioSource.clip = audioClips[2]; // 도망가는 소리 재생
            audioSource.Play();
            nav.SetDestination(originPosition); // 원래 이치로 이동시키기
        }
        yield return YieldInstructionCache.WaitForSeconds(5f);
        if (isThird == false)
        {
            isThird = true;
            playerController.enabled = true;
            zoomOut.SetActive(false);
            basicCam.SetActive(true);
            Destroy(gameObject);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && nav.speed != 0f) 
        {
            Debug.Log("게임오버!!!!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == stage4 && checkSight.isDetected)
        {
            isSpurn = true;
            isChaser = false;
        }
    }
}