using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutTint : MonoBehaviour {

    // Use this for initialization
    //Create left and right buttons that will change tint
    /*public Dropdown dropDownOne;
    public Dropdown dropDownTwo;*/

    /*public Texture2D red;
    public Texture2D yellow;
    public Texture2D green;
    public Texture2D blue;*/

    public Slider p1r;
    public Slider p1g;
    public Slider p1b;
    public Slider p2r;
    public Slider p2g;
    public Slider p2b;

    public Material p1Color;
    public Material p2Color;

    void Start ()
    {
        p1Color.color = Color.white;
        p2Color.color = Color.white;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Assign the material to the corresponding value
        // save the Material of each object and store it in a value -- this will update every frame
        /*Material p1Material = GameObject.FindGameObjectWithTag("P1").GetComponent<Renderer>().material;
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
        }*/
    }

    public void P1ColorChanged()
    {
        p1Color.color = new Color(p1r.value, p1g.value, p1b.value);
    }

    public void P2ColorChanged()
    {
        p2Color.color = new Color(p2r.value, p2g.value, p2b.value);
    }
}
