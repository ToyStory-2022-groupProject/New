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
        anim.SetBool("Pick", false);
        anim.SetBool("Grab", false);
        anim.SetBool("Move", false);
        anim.SetBool("Jump", false);
        playerController.enabled = false;
        PlayerController.isGrab = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical") && SpinnerManager.isChange)
        {
            uiSpinner[curSpinner].Spin(Convert.ToInt32(Input.GetAxisRaw("Vertical")));
        }
        
        if (Input.GetButtonDown("Horizontal") && SpinnerManager.isChange)
        {
            beforeSpinner = curSpinner;
            curSpinner += 1 * Convert.ToInt32(Input.GetAxisRaw("Horizontal"));

            if (curSpinner > 5)
                curSpinner = 0;
            
            if (curSpinner < 0)
                curSpinner = 5;

            SpinnerManager.scaleAdj(uiSpinner[beforeSpinner], uiSpinner[curSpinner]);
        }

        if (Input.GetKeyDown(KeyCode.Return) && isInputStop == false)
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
