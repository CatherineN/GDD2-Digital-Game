using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    float timeLeft = 5f;
    //Get the public variables for changing the textures
    public Canvas canvas;
    public Texture playerOneWin;
    public Texture playerTwoWin;
    public Texture computerWin;
    private void Awake()
    {
        //Set the texture based on who won
        if(PlayerPrefs.GetInt("winner") == 1)
            canvas.GetComponent<RawImage>().texture = playerOneWin;
        if (PlayerPrefs.GetInt("winner") == 2)
            canvas.GetComponent<RawImage>().texture = playerTwoWin;
        if (PlayerPrefs.GetInt("winner") == 3)
            canvas.GetComponent<RawImage>().texture = computerWin;

    }
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
