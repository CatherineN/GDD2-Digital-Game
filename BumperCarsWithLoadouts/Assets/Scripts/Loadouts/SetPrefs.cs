﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrefs : MonoBehaviour {

    float weight1;
    float weight2;
    string type1;
    string type2;
    string weapon1;
    string weapon2;

    // textures
    // W = wood; medium-weight
    // M = metal; heavyweight
    

    private void Awake()
    {
        //store the player prefs in local variables
        GetPrefences();

        //set each player's car to reflect the preferences they chose in the loadout menu
        if (gameObject.name == "PlayerCar")
        {
            SetPreferences(type1, weight1, weapon1);

        }
        else if (gameObject.name == "PlayerCar2")
        {
            SetPreferences(type2, weight2, weapon2);
        }
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Gets the player preferences and stores them in local variables
    /// Allows the setting of each player's car to match up with what they selected in the loadouts screen
    /// </summary>
    void GetPrefences()
    {
        // load the player car type
        weight1 = PlayerPrefs.GetFloat("weight1");
        type1 = PlayerPrefs.GetString("type1");

        weight2 = PlayerPrefs.GetFloat("weight2");
        type2 = PlayerPrefs.GetString("type2");

        // load the player weapon
        weapon1 = PlayerPrefs.GetString("weapon1");
        weapon2 = PlayerPrefs.GetString("weapon2");
    }

    /// <summary>
    /// Sets up the Car this script is attached to with the loadout they selected
    /// EDIT: No longer needs color as that is determined by the material
    /// </summary>
    /// <param name="color">Color of the car</param>
    /// <param name="type">Which class the car is (lightweight, normal, or heavyweight)</param>
    /// <param name="weight">The mass of the car, determined by its class</param>
    /// <param name="weapon">Which weapon they chose (bomb or cannon)</param>
    void SetPreferences(string type, float weight, string weapon)
    {

        // loads in the saved weapon
        switch (weapon)
        {
            case "Bomb Dropper":
                transform.GetChild(0).gameObject.SetActive(false);//paintbang
                transform.GetChild(1).gameObject.SetActive(false);//cannon
                transform.GetChild(2).gameObject.SetActive(true);//bomb
                transform.GetChild(3).gameObject.SetActive(false);//rocket punch
                break;

            case "Cannon":
                transform.GetChild(0).gameObject.SetActive(false);//paintbang
                transform.GetChild(1).gameObject.SetActive(true);//cannon
                transform.GetChild(2).gameObject.SetActive(false);//bomb
                transform.GetChild(3).gameObject.SetActive(false);//rocket punch
                break;
            case "PaintBang":
                transform.GetChild(0).gameObject.SetActive(true);//paintbang
                transform.GetChild(1).gameObject.SetActive(false);//cannon
                transform.GetChild(2).gameObject.SetActive(false);//bomb
                transform.GetChild(3).gameObject.SetActive(false);//rocket punch
                break;
            case "Rocket Punch":
                transform.GetChild(0).gameObject.SetActive(false);//paintbang
                transform.GetChild(1).gameObject.SetActive(false);//cannon
                transform.GetChild(2).gameObject.SetActive(false);//bomb
                transform.GetChild(3).gameObject.SetActive(true);//rocket punch
                break;
        }

        gameObject.GetComponent<Rigidbody>().mass = weight;

        // loads in the saved weight
        if (gameObject.GetComponent<Rigidbody>().mass == 0)
        {
            gameObject.GetComponent<Rigidbody>().mass = 50;
        }
    }
}
