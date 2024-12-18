using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeElapsed = 0f;
    float bestTime = Mathf.Infinity;
    private bool timerRunning = false;

    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text bestTimeText;

    void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
        }

        if (timerText != null)
        {
            timerText.text = timeElapsed.ToString("F2");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            timerRunning = true;
            timeElapsed = 0f;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            timerRunning = false;
            UpdateBestTime();
        }
    }

    void UpdateBestTime()
    {
        if(bestTime > timeElapsed)
        {
            bestTime = timeElapsed;
            bestTimeText.text = bestTime.ToString("F2");
        }
    }
}
