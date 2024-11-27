using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;
    private bool timerRunning = false;

    [SerializeField] TMP_Text timerText;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            timerRunning = true;
            timeElapsed = 0f;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            timerRunning = false;
        }
    }
}
