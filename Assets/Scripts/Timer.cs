//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Timer : MonoBehaviour
//{
//    public Image timer;

//    float time = 1f;

//    float sec = 20f;

//    void Start()
//    {

//    }

//    public void StartTimer()
//    {
//        time = 1f;
//        StartCoroutine("CountDown");
//    }

//    IEnumerator CountDown()
//    {
//        while (sec >= 0)
//        {
//            time -= 0.05f;
//            timer.fillAmount = time;
//            sec -= 1f;
//            yield return new WaitForSeconds(1f);
//        }

//    }
//}


using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timerText; // Reference to the Text GameObject

    [SerializeField]
    private float maxTime =30f; // Maximum time for the countdown

    private float currentTime; // Current time remaining
    private bool isRunning = false; // Flag to indicate if the timer is running

    // Start the timer
    public void StartTimer()
    {
        currentTime = maxTime;
        isRunning = true;
        StartCoroutine(Countdown());
    }

    // Stop the timer
    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }

    // Coroutine for the countdown logic
    private IEnumerator Countdown()
    {
        while (isRunning && currentTime > 0)
        {
            UpdateTimerDisplay();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }

        // Timer has reached zero or stopped
        UpdateTimerDisplay();
        isRunning = false;
    }

    // Update the timer text display
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime);
        }
    }

    // Format the time in MM:SS format
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Optional: Reset the timer to its initial state
    public void ResetTimer()
    {
        StopTimer();
        StartTimer();
    }

    // Optional: Pause the timer
    public void PauseTimer()
    {
        isRunning = false;
    }

    // Optional: Resume the timer
    public void ResumeTimer()
    {
        isRunning = true;
        StartCoroutine(Countdown());
    }
}