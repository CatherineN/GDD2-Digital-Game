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

    // Use this for initialization
    void Start () {
        if(gameObject.name == "PlayerCar")
        {
            // loads in the saved color and type
            switch(color1)
            {
                case "red":

                    if(type1 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = redW;
                    else if(type1 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = redM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = red;

                    break;

                case "green":

                    if (type1 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = greenW;
                    else if (type1 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = greenM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = green;

                    break;

                case "blue":

                    if (type1 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = blueW;
                    else if (type1 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = blueM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = blue;

                    break;

                case "yellow":

                    if (type1 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = yellowW;
                    else if (type1 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = yellowM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = yellow;

                    break;
            }
            // loads in the saved weapon
           /* switch (weapon1)
            {
                case "Bomb":
                    transform.FindChild("Bomb").gameObject.SetActive(false);
                    transform.FindChild("Plow").gameObject.SetActive(true);
                    break;

                case "Cannon":
                    transform.FindChild("Bomb").gameObject.SetActive(false);
                    transform.FindChild("Cannon").gameObject.SetActive(true);
                    break;
            }
            */ 

            // loads in the saved weight
            gameObject.GetComponent<Rigidbody>().mass = weight1;
            
        }
        else if(gameObject.name == "PlayerCar2")
        {
            // loads in saved color
            switch (color2)
            {
                case "red":
                    if (type2 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = redW;
                    else if (type2 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = redM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = red;
                    break;

                case "green":
                    if (type2 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = greenW;
                    else if (type2 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = greenM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = green;
                    break;

                case "blue":
                    if (type2 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = blueW;
                    else if (type2 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = blueM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = blue;
                    break;

                case "yellow":
                    if (type2 == "medium")
                        gameObject.GetComponent<Renderer>().material.mainTexture = yellowW;
                    else if (type2 == "heavy")
                        gameObject.GetComponent<Renderer>().material.mainTexture = yellowM;
                    else
                        gameObject.GetComponent<Renderer>().material.mainTexture = yellow;
                    break;
            }

            // loads in the saved weapon
            switch (weapon2)
            {
                case "Plow":
                    transform.FindChild("Cannon").gameObject.SetActive(false);
                    transform.FindChild("Plow").gameObject.SetActive(true);
                    break;

                case "Cannon":
                    transform.FindChild("Plow").gameObject.SetActive(false);
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
