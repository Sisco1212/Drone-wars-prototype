using UnityEngine;
using TMPro;
using System;


public class TImer : MonoBehaviour
{
    public TMP_Text timerText;
    public float time = 600f;

    void UpdateTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        timerText.text = string.Format("{0:D2}, {1:D2}", timeSpan.Minutes, timeSpan.Seconds);

    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            UpdateTime();
        }
        else {
            time = 0;
        }
    }
}
