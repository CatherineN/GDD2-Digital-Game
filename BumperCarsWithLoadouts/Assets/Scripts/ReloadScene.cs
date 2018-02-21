using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
