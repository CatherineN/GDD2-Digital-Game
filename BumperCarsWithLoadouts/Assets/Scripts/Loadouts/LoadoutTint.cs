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

    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Assign the material to the corresponding value
        // save the Material of each object and store it in a value -- this will update every frame
        Material p1Material = GameObject.FindGameObjectWithTag("P1").GetComponent<Renderer>().material;
        Material p2Material = GameObject.FindGameObjectWithTag("P2").GetComponent<Renderer>().material;

        switch (dropDownOne.captionText.text)
        {
            case "Red":
                p1Material.mainTexture = red;
                PlayerPrefs.SetString("color1", "red");
                break;
            case "Blue":
                p1Material.mainTexture = blue;
                PlayerPrefs.SetString("color1", "blue");
                break;
            case "Green":
                p1Material.mainTexture = green;
                PlayerPrefs.SetString("color1", "green");
                break;
            case "Yellow":
                p1Material.mainTexture = yellow;
                PlayerPrefs.SetString("color1", "yellow");
                break;
        }

        switch (dropDownTwo.captionText.text)
        {
            case "Red":
                p2Material.mainTexture = red;
                PlayerPrefs.SetString("color2", "red");
                break;
            case "Blue":
                p2Material.mainTexture = blue;
                PlayerPrefs.SetString("color2", "blue");
                break;
            case "Green":
                p2Material.mainTexture = green;
                PlayerPrefs.SetString("color2", "green");
                break;
            case "Yellow":
                p2Material.mainTexture = yellow;
                PlayerPrefs.SetString("color2", "yellow");
                break;
        }
    }
}
