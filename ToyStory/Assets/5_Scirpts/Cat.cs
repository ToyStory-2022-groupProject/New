using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;


public class Cat : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject player;
    public GameObject head; // 플레이어 위치카메라
    public float distance; // 캐릭터와 고양이 사이 거리
    private bool isPlayer;
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
    public AudioClip[] audioClips; // 0 = 일어나는 소리 1 = 공격소리


    private bool isAudioPlayed;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindPlayer();
        if (isPlayer)
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

            
            // 게임오버!!!
        }
        if(nav.enabled)
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
    }

    void FindPlayer() // 범위 외로 이동하려고 하는 경우
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.magenta);
        isPlayer = Physics.Raycast(transform.position, transform.forward, distance, 
            LayerMask.GetMask("Player"));
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && nav.speed != 0f) 
        {
            Debug.Log("게임오버!!!!");
        }
    }
}