using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text[] timerLabels;//the UI element used to display the timer
    public float timeLeft;//time in the timer in seconds
    public string endMessage;//the string that is displayed once the timer ticks down to 0

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;

        int minutes = (int)timeLeft / 60;
        int seconds = (int)timeLeft % 60;

        //update the label
        foreach (Text label in timerLabels)
        {
            if (timeLeft <= 0)//if timer is less than 0 display end message
            {
                label.text = endMessage;
            }
            else if(minutes > 0)//else display the time remaining properly formatted
            {
                label.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                label.text = string.Format("{0:00}", seconds);
            }
        }

        
    }
}
