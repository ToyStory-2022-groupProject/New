using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위함

public class NoiseCheck : MonoBehaviour
{
    [SerializeField] private GameObject player; 
    // 캐릭터 오브젝트
    [SerializeField] private GameObject UIObject; 
    // Canvas오브젝트
    [SerializeField] private Slider noiseSlider;
    // 슬라이더 오브젝트
    [SerializeField] private Image fillImage;
    // 슬라이더의 값에 따라 슬라이더의 색에 변화를 주기위한 이미지

    // 캐릭터가 들어올때만 수행시키기
    private bool isPlayerEnter;
    
    private float chargeSpeed; 
    // 채워지는 속도
    private float chargeTime; 
    // 채워지는데 걸리는 시간
    private float currentNoise; 
    // 현재 슬라이더 값을 저장

    public float walkingTime = 15f;
    // 캐릭터가 걸을 때
    public float runningTime = 3f;
    // 캐릭터가 달릴 때의 chargeTime
    public float waitingTime = 4f;
    // 캐릭터가 대기하고 있을 때
    public float sliderYOffset = 6f;
    // 캐릭터와 슬라이더 사이의 고정 오프셋
    
    void Update()
    {
        if (isPlayerEnter)
        {
            // 캐릭터가 움직이면 UI도 동시에 움직일 수 있도록 수행
            UIObject.transform.position = new Vector3(player.transform.position.x,
                player.transform.position.y + sliderYOffset,
                player.transform.position.z);

            // 트리거 내에서 캐릭터 움직임 관련 입력을 받으면 수행    
            if (Input.GetKey(KeySetting.keys[KeyAction.LEFT]) || Input.GetKey(KeySetting.keys[KeyAction.RIGHT]) ||
                Input.GetKey(KeySetting.keys[KeyAction.UP]) || Input.GetKey(KeySetting.keys[KeyAction.Down]))
            {
                if (Input.GetKey(KeySetting.keys[KeyAction.WALK]))
                {
                    chargeTime = walkingTime;
                }
                else
                {
                    chargeTime = runningTime;
                }
            }
            else
            {
                chargeTime = -waitingTime;
            }

            // 속도 = 거리 / 시간 => 
            // 채워지는 속도 = (슬라이더 최대값 - 최소값) / 상황에 따라 설정한 시간값
            chargeSpeed = (noiseSlider.maxValue - noiseSlider.minValue) / chargeTime;

            // 실제 시간과 똑같이 걸릴 수 있도록 Time.deltaTime을 곱하기
            currentNoise += chargeSpeed * Time.deltaTime;

            // 현재 값이 슬라이더 최대값 이상이면 최대값으로
            if (currentNoise >= noiseSlider.maxValue)
            {
                currentNoise = noiseSlider.maxValue;
            }
            // 현재 값이 슬라이더 최소값 이하라면 최소값으로
            else if (currentNoise <= noiseSlider.minValue)
            {
                currentNoise = noiseSlider.minValue;
            }

            ColorChange();
            // 슬라이더에 적용
            noiseSlider.value = currentNoise;
        }
    }

	// 슬라이더의 현재값에 따라 적용할 색깔 지정
    void ColorChange() 
    {
        if(currentNoise >= 0f && currentNoise <= 40f)
            fillImage.color = Color.green;
        else if(currentNoise > 40f && currentNoise <= 70f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;
    }

	// 캐릭터가 트리거에 들어오면 UI오브젝트 켜기
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIObject.SetActive(true);
            isPlayerEnter = true;
        }
    }


	// 캐릭터가 트리거에 들어오면 UI오브젝트 끄기
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {            
            // 다시 들어올 경우를 대비해 현재 소음 값 초기화 시켜놓기
            currentNoise = noiseSlider.minValue;
            UIObject.SetActive(false);
            isPlayerEnter = false;
        }
    }
}