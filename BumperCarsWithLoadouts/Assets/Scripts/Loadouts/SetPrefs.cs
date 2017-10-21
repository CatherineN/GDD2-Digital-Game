using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrefs : MonoBehaviour {

    string color1;
    string color2;
    float weight1;
    float weight2;
    string type1;
    string type2;
    string weapon1;
    string weapon2;

    // textures
    // W = wood; medium-weight
    // M = metal; heavyweight
    public Texture2D red;
    public Texture2D redW;
    public Texture2D redM;

    public Texture2D yellow;
    public Texture2D yellowW;
    public Texture2D yellowM;

    public Texture2D green;
    public Texture2D greenW;
    public Texture2D greenM;

    public Texture2D blue;
    public Texture2D blueW;
    public Texture2D blueM;
    

    private void Awake()
    {
        //store the player prefs in local variables
        GetPrefences();

        //set each player's car to reflect the preferences they chose in the loadout menu
        if (gameObject.name == "PlayerCar")
        {
            SetPreferences(color1, type1, weight1, weapon1);

        }
        else if (gameObject.name == "PlayerCar2")
        {
            SetPreferences(color2, type2, weight2, weapon2);
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
        // load the player color
        color1 = PlayerPrefs.GetString("color1");
        color2 = PlayerPrefs.GetString("color2");

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
    /// </summary>
    /// <param name="color">Color of the car</param>
    /// <param name="type">Which class the car is (lightweight, normal, or heavyweight)</param>
    /// <param name="weight">The mass of the car, determined by its class</param>
    /// <param name="weapon">Which weapon they chose (bomb or cannon)</param>
    void SetPreferences(string color, string type, float weight, string weapon)
    {
        // loads in saved color
        switch (color)
        {
            case "red":
                if (type == "medium")
                    gameObject.GetComponent<Renderer>().material.mainTexture = redW;
                else if (type == "heavy")
                    gameObject.GetComponent<Renderer>().material.mainTexture = redM;
                else
                    gameObject.GetComponent<Renderer>().material.mainTexture = red;
                break;

            case "green":
                if (type == "medium")
                    gameObject.GetComponent<Renderer>().material.mainTexture = greenW;
                else if (type == "heavy")
                    gameObject.GetComponent<Renderer>().material.mainTexture = greenM;
                else
                    gameObject.GetComponent<Renderer>().material.mainTexture = green;
                break;

            case "blue":
                if (type == "medium")
                    gameObject.GetComponent<Renderer>().material.mainTexture = blueW;
                else if (type == "heavy")
                    gameObject.GetComponent<Renderer>().material.mainTexture = blueM;
                else
                    gameObject.GetComponent<Renderer>().material.mainTexture = blue;
                break;

            case "yellow":
                if (type == "medium")
                    gameObject.GetComponent<Renderer>().material.mainTexture = yellowW;
                else if (type == "heavy")
                    gameObject.GetComponent<Renderer>().material.mainTexture = yellowM;
                else
                    gameObject.GetComponent<Renderer>().material.mainTexture = yellow;
                break;
        }

        // loads in the saved weapon
        switch (weapon)
        {
            case "Bomb Dropper":
                transform.FindChild("Cannon").gameObject.SetActive(false);
                transform.FindChild("Bomb Dropper").gameObject.SetActive(true);
                break;

            case "Cannon":
                transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                transform.FindChild("Cannon").gameObject.SetActive(true);
                break;
        }

        // loads in the saved weight
        gameObject.GetComponent<Rigidbody>().mass = weight;
    }
}
