using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameObject Player;
    public Image Shade;
    CheckPointer checkPointer;

    [SerializeField]
    [Range(0.01f, 5f)]
    private float fadeTime;
    [SerializeField]
    private AnimationCurve fadeCurve;

    void Start()
    {
        checkPointer = GetComponent<CheckPointer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Shade = GetComponent<Image>();
    }

    // Update is called once per frame()
    void Update()
    {

        
    }

    public void Restart()
    {
        Replace();

    }
    public void Replace()
    {
        checkPointer.FindCheckPoint();
        Player.transform.position = checkPointer.checkPoint[checkPointer.pointNum].transform.position;
    }

    private IEnumerator FadeIn(float Start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = Shade.color;
            color.a = Mathf.Lerp(Start, end, fadeCurve.Evaluate(percent));
            Shade.color = color;

            yield return null;
        }
    }
    private void FadeOut()
    {

    }
}
