using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public Image timerImage;

    [SerializeField]
    private GameObject clock;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private float maxTimer = 20f;

    private Coroutine timerCoroutine;

    private void Start()
    {
        // Start the timer coroutine
        timerCoroutine = StartCoroutine(CountDownTimer());
    }

    private IEnumerator CountDownTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < maxTimer)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime++;

            // Update the fill amount of the timer image
            timerImage.fillAmount = elapsedTime / maxTimer;
        }

        gameOverPanel.SetActive(true);
        Game.instance.panelPlayer1.SetActive(false);
        Game.instance.panelPlayer2.SetActive(false);
        clock.SetActive(false);
        // Timer has reached zero, reset it
        ResetTimer();
    }

    public void ResetTimer()
    {
        // Stop the timer coroutine
        StopCoroutine(timerCoroutine);

        // Restart the timer coroutine
        timerCoroutine = StartCoroutine(CountDownTimer());
    }

    private void OnDisable()
    {
        // Stop the timer coroutine when the script is disabled
        StopCoroutine(timerCoroutine);
    }

    public void StopTimer()
    {
        // Stop the timer coroutine
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }
}
