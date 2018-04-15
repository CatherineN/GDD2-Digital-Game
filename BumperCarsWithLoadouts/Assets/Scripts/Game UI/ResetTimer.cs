using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetTimer : Timer {

    public float timeBtwEvents;
    public float eventLength;

	// Use this for initialization
	void Start () {
        timeLeft = timeBtwEvents;
	}
	
	// Update is called once per frame
	public override void Update () {

        base.Update();
        
        if (timeLeft <= 0)//if timer is less than 0 display event message
        {
            foreach (Text label in timerLabels)
            {
                if (timeLeft <= 0 - eventLength)
                {
                    //reset the timer to countdown again once the event is over
                    timeLeft = timeBtwEvents;
                    label.text = countMsg + FormatTime(timeLeft);
                }
                else
                {
                    label.text = endMessage;
                }
            }
            
        }
    }
}
