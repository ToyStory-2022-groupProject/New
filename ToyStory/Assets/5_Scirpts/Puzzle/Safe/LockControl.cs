using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    [SerializeField] private SpinnerManager[] uiSpinner;
    [SerializeField] private GameObject[] realSpinner;
    [SerializeField] private int[] answer = {0, 0, 0, 0, 0, 0};
    [SerializeField] private GameObject[] checker;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject noiseCheck;
    
    private int curSpinner;
    private int beforeSpinner;
    private MeshRenderer uiMeshRenderer;
    private MeshRenderer realMeshRenderer;
    private Material uiMaterial;
    private Material realMaterial;
    private bool isInputStop;
    private int vDir;
    private int hDir;
    private bool init = true;
    
    private void Start()
    {
        curSpinner = 0;
        
        uiMeshRenderer = checker[0].GetComponent<MeshRenderer>();
        uiMaterial = uiMeshRenderer.material;
        
        realMeshRenderer = checker[1].GetComponent<MeshRenderer>();
        realMaterial = realMeshRenderer.material;
    }

    private void OnEnable()
    {
        SFXMgr.Instance.Stop_SFX();
        // anim.SetBool("Pick", false);
        // anim.SetBool("Grab", false);
        anim.SetBool("Move", false);
        anim.SetBool("Jump", false);
        playerController.Switch = false;
        playerController.enabled = false;
        PlayerController.isGrab = false;
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeySetting.keys[KeyAction.UP]) || Input.GetKeyDown(KeySetting.keys[KeyAction.Down])) && SpinnerManager.isChange && isInputStop == false)
        {
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.UP]))
            {
                vDir = 1;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.Down]))
            {
                vDir = -1;
            }
            uiSpinner[curSpinner].Spin(vDir);
        }
        
        if(Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT]) || Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT]) && SpinnerManager.isChange && isInputStop == false)
        {
            if (Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT]))
            {
                hDir = -1;
            }

            if (Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT]))
            {
                hDir = 1;
            }
            beforeSpinner = curSpinner;
            curSpinner += 1 * hDir;

            if (curSpinner > 5)
                curSpinner = 0;
            
            if (curSpinner < 0)
                curSpinner = 5;

            SpinnerManager.scaleAdj(uiSpinner[beforeSpinner], uiSpinner[curSpinner]);
        }

        if(Input.GetKeyDown(KeySetting.keys[KeyAction.GRAB]) && isInputStop == false)
        {
            if (init)
            {
                init = false;
            }
            else
            {
                if (Correct())
                {
                    StartCoroutine(Clear());
                }
                else
                {
                    uiMaterial.color = Color.red;
                    realMaterial.color = Color.red;
                    audioSource.clip = audioClip[1];
                    audioSource.Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitUI();
        }
    }

    void QuitUI()
    {
        gameObject.SetActive(false);
        panel.SetActive(false);
        Safe.isSafePuzzle = false;
        playerController.enabled = true;
        playerController.Switch = false;
        anim.SetBool("Switch", false);
        noiseCheck.SetActive(true);
        uiMaterial.color = Color.white;
        realMaterial.color = Color.white;
    }

    IEnumerator Clear()
    {
        isInputStop = true;
        uiMaterial.color = Color.green;
        realMaterial.color = Color.green;
        audioSource.clip = audioClip[0];
        audioSource.Play();
        Safe.isSafePuzzleClear = true;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        panel.SetActive(false);
        gameObject.SetActive(false);
        noiseCheck.SetActive(false);
    }
    
    bool Correct()
    {
        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] != uiSpinner[i].spinNum)
            {
                return false;
            }
        }
        return true;
    }
    
    
}
