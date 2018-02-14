using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public Dropdown stageSelect;
    public Dropdown enabledAI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {

	}

    public void Load()
    {
        if (enabledAI.value == 1)
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
                SceneManager.LoadScene("DefaultArena");
            else if (stageSelect.value == 1)
                SceneManager.LoadScene("IceArena");
            else if (stageSelect.value == 2)
                SceneManager.LoadScene("CollapsingArena");
        }
    }
    
}
