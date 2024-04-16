using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image timer;

    float time = 1f;

    float sec = 20f;

    void Start()
    {
        
    }

    public void StartTimer()
    {
        time = 1f;
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        while(sec >= 0)
        {
            time -= 0.05f;
            timer.fillAmount = time;
            sec -= 1f;
            yield return new WaitForSeconds(1f);
        }
        
    }
}
