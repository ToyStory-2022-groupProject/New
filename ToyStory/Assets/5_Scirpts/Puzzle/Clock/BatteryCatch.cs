using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


public class BatteryCatch : MonoBehaviour
{
    public static bool isStop;

    public Image hitBox;
    public Image batteryVeilImage; 
    public GameObject panel;
    public PlayerController playerController;
    public GameObject battery;
    public AudioSource audioSource;
    public Slider slider;
    public Animator anim;
    public BoxCollider clockTrigger;
    
    public float wantTime;
    public float errorRange;
    
    private Random randomNum;
    private float handSpeed;
    private float direction;
    private int randomPositionX;

    private void Start()
    {
        randomNum = new Random();
        randomPositionX = randomNum.Next(150, 850);
        handSpeed = slider.maxValue / wantTime;
        hitBox.rectTransform.anchoredPosition = new Vector2(randomPositionX, hitBox.rectTransform.anchoredPosition.y);
    }

    private void OnEnable()
    {
        SFXMgr.Instance.Stop_SFX();
        anim.SetBool("Pick", false);
        anim.SetBool("Grab", false);
        anim.SetBool("Move", false);
        anim.SetBool("Jump", false);


        PlayerController.isGrab = false;
        playerController.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.GRAB]))
        {
            HitCheck();
            randomPositionX = randomNum.Next(150, 850);
            hitBox.rectTransform.anchoredPosition = new Vector2(randomPositionX, hitBox.rectTransform.anchoredPosition.y);
        }
        
        if (batteryVeilImage.fillAmount >= 1f && isStop == false)
        {
            isStop = true;
            StartCoroutine(Clear());
        }
    }

    void FixedUpdate()
    {
        if (isStop == false)
        {
            BatteryMove();
        }
    }

    void HitCheck()
    {
        if (hitBox.rectTransform.anchoredPosition.x + errorRange >= slider.value && hitBox.rectTransform.anchoredPosition.x - errorRange <= slider.value)
        {
            batteryVeilImage.fillAmount += 0.2f;
        }

    }

    void BatteryMove()
    {
        if (slider.value <= slider.minValue)
        {
            direction = +1;
        }
        else if(slider.value >= slider.maxValue)
        {
            direction = -1;
        }
        slider.value += handSpeed * direction * Time.deltaTime;
    }
    

    IEnumerator Clear()
    {
        battery.SetActive(false);
        audioSource.Play();
        yield return YieldInstructionCache.WaitForSeconds(1f);
        panel.SetActive(false);
        clockTrigger.enabled = false;
        playerController.enabled = true;
        gameObject.SetActive(false);
    }
}
