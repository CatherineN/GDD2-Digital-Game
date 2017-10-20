using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrefs : MonoBehaviour {

    string color1;
    string color2;
    float weight1;
    float weight2;
    string weapon1;
    string weapon2;

    public Texture2D red;
    public Texture2D yellow;
    public Texture2D green;
    public Texture2D blue;

    private void Awake()
    {
        // load the player color
        color1 = PlayerPrefs.GetString("color1");
        color2 = PlayerPrefs.GetString("color2");

        // load the player car type
        weight1 = PlayerPrefs.GetFloat("weight1");
        weight2 = PlayerPrefs.GetFloat("weight2");

        // load the player weapon
        weapon1 = PlayerPrefs.GetString("weapon1");
        weapon2 = PlayerPrefs.GetString("weapon2");
    }

    // Use this for initialization
    void Start () {
        if(gameObject.name == "PlayerCar")
        {
            // loads in the saved color
            switch(color1)
            {
                case "red":
                    gameObject.GetComponent<Renderer>().material.mainTexture = red;
                    break;

                case "green":
                    gameObject.GetComponent<Renderer>().material.mainTexture = green;
                    break;

                case "blue":
                    gameObject.GetComponent<Renderer>().material.mainTexture = blue;
                    break;

                case "yellow":
                    gameObject.GetComponent<Renderer>().material.mainTexture = yellow;
                    break;
            }
            // loads in the saved weapon
            switch (weapon1)
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
            gameObject.GetComponent<Rigidbody>().mass = weight1;
            
        }
        else if(gameObject.name == "PlayerCar2")
        {
            // loads in saved color
            switch (color2)
            {
                case "red":
                    gameObject.GetComponent<Renderer>().material.mainTexture = red;
                    break;

                case "green":
                    gameObject.GetComponent<Renderer>().material.mainTexture = green;
                    break;

                case "blue":
                    gameObject.GetComponent<Renderer>().material.mainTexture = blue;
                    break;

                case "yellow":
                    gameObject.GetComponent<Renderer>().material.mainTexture = yellow;
                    break;
            }
            // loads in the saved weapon
            switch (weapon2)
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
            gameObject.GetComponent<Rigidbody>().mass = weight1;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
