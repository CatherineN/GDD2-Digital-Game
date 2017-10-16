using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    float timeLeft = 5f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Back to Start in: " + (int)timeLeft;
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
