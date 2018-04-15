using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text[] timerLabels;//the UI element used to display the timer
    public float timeLeft;//time in the timer in seconds
    public string endMessage;//the string that is displayed once the timer ticks down to 0
    public string countMsg;//the string that is displayed while the timer ticks down to 0

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    public virtual void Update () {
        timeLeft -= Time.deltaTime;

        string formattedTime = FormatTime(timeLeft);

        //update the label
        foreach (Text label in timerLabels)
        {
            if (timeLeft <= 0)//if timer is less than 0 display end message
            {
                label.text = endMessage;
            }
            else
            {
                label.text = countMsg + formattedTime;
            }
        }

        
    }

    /// <summary>
    /// Formats a float into a formatted string in minutes and seconds using stardard format
    /// </summary>
    /// <param name="time">The time that needs to be formatted into minutes and seconds</param>
    /// <returns>Time in the format of min:secs</returns>
    protected string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        if (minutes > 0)//else display the time remaining properly formatted
        {
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (seconds >= 10)
        {
            return string.Format("{0:00}", seconds);
        }
        else
        {
            return string.Format("{0:0}", seconds);
        }
    }
}
