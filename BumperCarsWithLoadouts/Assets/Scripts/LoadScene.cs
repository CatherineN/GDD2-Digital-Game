using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    bool controls = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // to make this script adaptable, it looks at the tag of the scene, and
        // then loads the appropriate scene.
        checkForControls();
        if (Input.GetButtonDown("Start"))
        {
            
            if (gameObject.tag == "Main Menu")
            {
                SceneManager.LoadScene("LoadOuts");
            }
            else if (gameObject.tag == "Loadout Menu")
            {
                SceneManager.LoadScene("DefaultArena");
            }
            else if(gameObject.tag == "gameOver")
            {
                SceneManager.LoadScene("Start");
            }
        }
        else if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("Fire2"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
        }
    }
    void checkForControls()
    {
        if (Input.GetButtonDown("Select"))
        {
            controls = !controls;
            if (controls)
            {
                GameObject.Find("Start").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Controls").GetComponent<Canvas>().enabled = true;
            }
            else
            {
                GameObject.Find("Start").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Controls").GetComponent<Canvas>().enabled = false;
            }
        }
        
    }

    // load the main menu when button on game over scene is pressed
    /*public void Load()
    {
        if(gameObject.tag == "gameOver")
        {
            SceneManager.LoadScene("Start");
        }
    }*/

}
