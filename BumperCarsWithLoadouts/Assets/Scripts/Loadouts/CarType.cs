﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarType : MonoBehaviour {

    // drag in buttons to edit the class of the car
    public Dropdown topDropdown;
    public Dropdown bottomDropdown;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // switch statement for player 1 selection
        switch (topDropdown.captionText.text)
        {
            case "Lightweight (low mass)": // decrease mass of car: mass = 45
                PlayerPrefs.SetFloat("weight1", 45f);
                break;

            case "Normal (mass = 1)": // mass is default value: mass = 50
                PlayerPrefs.SetFloat("weight1", 50f);
                break;

            case "Heavyweight (high mass)": // increase mass of car: mass = 55
                PlayerPrefs.SetFloat("weight1", 55f);
                break;
        }

        // switch statement for player 2 selection
        switch (topDropdown.captionText.text)
        {
            case "Lightweight (low mass)":
                PlayerPrefs.SetFloat("weight2", 45f);
                break;

            case "Normal (mass = 1)":
                PlayerPrefs.SetFloat("weight2", 50f);
                break;

            case "Heavyweight (high mass)":
                PlayerPrefs.SetFloat("weight2", 55f);
                break;
        }
    }
}