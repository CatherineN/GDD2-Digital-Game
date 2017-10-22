using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public Dropdown stageSelect;
    public Toggle enabledAI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {

	}

    public void Load()
    {
        if (enabledAI.isOn)
        {
            PlayerPrefs.SetInt("numAI", 5);
        }
        else
        {
            PlayerPrefs.SetInt("numAI", 0);
        }
        if (gameObject.tag == "Loadout Menu")
        {
            if (stageSelect.value == 0)
                SceneManager.LoadScene("Bob");
            else
                SceneManager.LoadScene("CameraTest");
        }
    }
    
}
