using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameObject Player;
    public CheckPointer checkPointer;
    public Image panel;

    void Start()
    {

    }

    // Update is called once per frame()
    void Update()
    {

        
    }

    public void Restart(float fadeout, float fadein)
    {
        StartCoroutine(FadeCoroutine(fadeout, fadein));
    }
    public void Replace()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        checkPointer = FindObjectOfType<CheckPointer>();
        checkPointer.FindCheckPoint();
        Player.transform.position = checkPointer.checkPoint[checkPointer.pointNum].transform.position;
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
        Replace();
        while(fadecount > 0.0f)
        {
            fadecount -= fadein;
            yield return new WaitForSeconds(0.01f);
            panel.color = new Color(0,0,0,fadecount);
        }
        Player.GetComponent<PlayerController>().enabled = true;
        yield return null;
    }

}
