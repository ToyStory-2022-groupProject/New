using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeCoroutine(float alpha)
    {
        float fadecount = 0;

        while(fadecount < 1.0f)
        {
            fadecount += alpha;
            yield return new WaitForSeconds(0.01f);
            panel.color = new Color(0,0,0,fadecount);
        }
    }
}
