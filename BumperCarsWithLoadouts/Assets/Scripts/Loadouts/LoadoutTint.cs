using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutTint : MonoBehaviour {

    // Use this for initialization
    //Create left and right buttons that will change tint
    public Dropdown dropDownOne;
    public Dropdown dropDownTwo;

    public Texture2D red;
    public Texture2D yellow;
    public Texture2D green;
    public Texture2D blue;
    //Create array of colors
    void Start ()
    {
	    //Assign values in array
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if the left button is clicked
        //decrease the enum value 
        //if the right button is clicked
        //increase the enum value
        //Assign the material to the corresponding enum value
        switch(dropDownOne.captionText.text)
        {
            case "Red":
                GameObject.FindGameObjectWithTag("P1").GetComponent<Renderer>().material.mainTexture = red;
                PlayerPrefs.SetString("color1", "red");
                break;
            case "Blue":
                GameObject.FindGameObjectWithTag("P1").GetComponent<Renderer>().material.mainTexture = blue;
                PlayerPrefs.SetString("color1", "blue");
                break;
            case "Green":
                GameObject.FindGameObjectWithTag("P1").GetComponent<Renderer>().material.mainTexture = green;
                PlayerPrefs.SetString("color1", "green");
                break;
            case "Yellow":
                GameObject.FindGameObjectWithTag("P1").GetComponent<Renderer>().material.mainTexture = yellow;
                PlayerPrefs.SetString("color1", "yellow");
                break;
        }

        switch (dropDownTwo.captionText.text)
        {
            case "Red":
                GameObject.FindGameObjectWithTag("P2").GetComponent<Renderer>().material.mainTexture = red;
                PlayerPrefs.SetString("color2", "red");
                break;
            case "Blue":
                GameObject.FindGameObjectWithTag("P2").GetComponent<Renderer>().material.mainTexture = blue;
                PlayerPrefs.SetString("color2", "blue");
                break;
            case "Green":
                GameObject.FindGameObjectWithTag("P2").GetComponent<Renderer>().material.mainTexture = green;
                PlayerPrefs.SetString("color2", "green");
                break;
            case "Yellow":
                GameObject.FindGameObjectWithTag("P2").GetComponent<Renderer>().material.mainTexture = yellow;
                PlayerPrefs.SetString("color2", "yellow");
                break;
        }
    }
}
