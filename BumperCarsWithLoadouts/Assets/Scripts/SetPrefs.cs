using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrefs : MonoBehaviour {

    string color1;
    string color2;
    float weight1;
    float weight2;

    private void Awake()
    {
        // load the player color
        color1 = PlayerPrefs.GetString("color1");
        color2 = PlayerPrefs.GetString("color2");

        // load the player car type
        weight1 = PlayerPrefs.GetFloat("weight1");
        weight2 = PlayerPrefs.GetFloat("weight2");

        // load the player weapon

    }

    // Use this for initialization
    void Start () {
        if(gameObject.name == "PlayerCar")
        {
            // loads in the saved color
            switch(color1)
            {
                case "red":
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    break;

                case "green":
                    gameObject.GetComponent<Renderer>().material.color = Color.green;
                    break;

                case "blue":
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    break;

                case "yellow":
                    gameObject.GetComponent<Renderer>().material.color = Color.yellow;
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
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    break;

                case "green":
                    gameObject.GetComponent<Renderer>().material.color = Color.green;
                    break;

                case "blue":
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    break;

                case "yellow":
                    gameObject.GetComponent<Renderer>().material.color = Color.yellow;
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
