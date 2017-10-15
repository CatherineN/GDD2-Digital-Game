using System.Collections;
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
            case "Lightweight": // decrease mass of car: mass = 45
                PlayerPrefs.SetFloat("weight1", 45);
                break;

            case "Normal": // mass is default value: mass = 50
                PlayerPrefs.SetFloat("weight1", 50);
                break;

            case "Heavyweight": // increase mass of car: mass = 55
                PlayerPrefs.SetFloat("weight1", 55);
                break;
        }

        // switch statement for player 2 selection
        switch (topDropdown.captionText.text)
        {
            case "Lightweight":
                PlayerPrefs.SetFloat("weight2", 45);
                break;

            case "Normal":
                PlayerPrefs.SetFloat("weight2", 50);
                break;

            case "Heavyweight":
                PlayerPrefs.SetFloat("weight2", 55);
                break;
        }
    }
}
