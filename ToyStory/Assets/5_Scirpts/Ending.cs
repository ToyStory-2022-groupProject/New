using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject player;
    public PlayableDirector playableDirector;
    public TimelineAsset timelineAsset;
    public GameObject puzzleKey;
    public GameObject animationKey;
    public Animator anim;
    public Vector3 targetPos;
    public bool isEnd;
    public BoxCollider boxCollider;
    public Image panel;
    public DataManager dataManager;
    public BoxCollider lastDoor;

    public Detected detected;
    
    private void Update()
    {
        if (isEnd)
        {
            lastDoor.enabled = false;
            anim.SetBool("Move", true);
            player.transform.Translate(new Vector3(0,0,0.1f * 0.3f));
            if (SFXMgr.SFX.volume >= 0f)
            {
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.Run);
                SFXMgr.SFX.volume -= 0.1f * Time.deltaTime;
            }
        }
    }

    public void First()
    {
        player.GetComponent<PlayerController>().enabled = false;
        anim.SetBool("Move", false);
        PlayerController.isGrab = false;
        anim.SetBool("Grab", false);
        SFXMgr.Instance.Stop_SFX();
        StartCoroutine(FadeCoroutine(0.05f, 0.01f));
        
    }
    public void Second()
    {
        isEnd = true;
    }
    IEnumerator FadeCoroutine(float fadeout, float fadein)
    {
        float fadecount = 0;
     
        while(fadecount < 1.0f)
        { 
            fadecount += fadeout; 
            yield return new WaitForSeconds(0.01f);
            panel.color = new Color(0,0,0,fadecount);
        }
        player.transform.position = targetPos;
        detected.enabled = false;
        puzzleKey.SetActive(false);
        animationKey.SetActive(true);
        while(fadecount > 0.0f)
        { 
            fadecount -= fadein;
            yield return new WaitForSeconds(0.01f); 
            panel.color = new Color(0,0,0,fadecount);
        }
        yield return null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == puzzleKey)
        {
            playableDirector.Play(timelineAsset);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnd = false;
            SFXMgr.Instance.Stop_SFX();
            SFXMgr.SFX.volume = 1;
            dataManager = FindObjectOfType<DataManager>();
            dataManager.Save();
            LoadingSceneController.Instance.LoadScene(0);
        }
    }
}
