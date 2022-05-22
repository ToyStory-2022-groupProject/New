using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject mainCam;
    Camera cam;
    bool isSee;
    
    [Serializable]
    public struct Offset
    {
        public Vector3 pos;
        public Quaternion rot;
    }

    public Offset[] offsets;

    void Awake()
    {
        isSee = false;
    }

    void Update()
    {
        if (isSee)
        {
            isSee = false;
            StartCoroutine(ConvertingTransform());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainCam.SetActive(false);
            player.GetComponent<PlayerController>().enabled = false;
            gameObject.AddComponent<Camera>();
            gameObject.AddComponent<AudioListener>();
            isSee = true;
        }
    }

    IEnumerator ConvertingTransform()
    {
        int i;
        for (i = 0; i < offsets.Length; i++)
        {
            gameObject.transform.position = offsets[i].pos;
            gameObject.transform.rotation = offsets[i].rot;  
            yield return new WaitForSeconds(2.5f);
        }

        if (i > 1)
        {
            player.GetComponent<PlayerController>().enabled = true;
            Destroy(gameObject);
            mainCam.SetActive(true);
        }
    }
}
