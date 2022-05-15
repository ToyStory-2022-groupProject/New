using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switcher: MonoBehaviour
{
    // 스위치 트리거 이용
    public enum Type
    {
        fan,
        pop,
    }
    public Type puzzleType;
    
    [Serializable]
    public struct PuzzleSet
    {
        public GameObject effect;
        public GameObject puzzleGameObject; // 퍼즐 관련 도구
        public GameObject[] puzzleGameObjects; // 퍼즐 관련 도구가 집합체일경우
        public float delayTime; // 지연시간이 필요한 경우
    }
    
    public PuzzleSet puzzleSet;
    
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (puzzleType)
            {
                case Type.fan:
                    puzzleSet.effect.SetActive(true);
                    foreach (GameObject obj in puzzleSet.puzzleGameObjects)
                    {
                        if(obj.gameObject != null)
                            obj.GetComponent<Fan>().Invoke("acting", puzzleSet.delayTime);
                    }
                    break;
                case Type.pop:
                    puzzleSet.puzzleGameObject.GetComponent<PuzzlePopping>().Invoke("acting", puzzleSet.delayTime);
                    break;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (puzzleType)
            {
                case Type.fan:
                    puzzleSet.puzzleGameObject.transform.Rotate(new Vector3(0,0,1) * Time.deltaTime*500);
                    break;
            }
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (puzzleType)
            {
                case Type.fan:
                    puzzleSet.effect.SetActive(false);
                    puzzleSet.puzzleGameObject.transform.Rotate((new Vector3(0, 0, 1)) * Time.deltaTime * 0);
                    foreach (GameObject obj in puzzleSet.puzzleGameObjects)
                    {
                        if (obj.gameObject != null)
                            obj.GetComponent<Fan>().deactivating();
                    }

                    break;
                case Type.pop:
                    //puzzleGameObject.GetComponent<PuzzlePopping>().deactivating();
                    break;
            }
        }
    }
}
