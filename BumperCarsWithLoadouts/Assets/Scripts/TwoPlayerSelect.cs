using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TwoPlayerSelect : MonoBehaviour {

    public Canvas menuSelect;
    public Button startButton;
    private bool p1Active = true;
    private List<Selectable> elements;
    private Selectable currentActiveElement;
    private int tagTracker;

	// Use this for initialization
	void Start () {
        elements = new List<Selectable>();
        elements = Selectable.allSelectables;
        tagTracker = 1;
        UpdateTagTracker();
        
	}
	
	// Update is called once per frame
	void Update () {
        InputManager();
	}
    void InputManager()
    {
        if (tagTracker <= 3)
        {
            if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
            {
                tagTracker++;
                UpdateTagTracker();
                Debug.Log("BEEP");

            }
        }
        if (tagTracker > 3 && tagTracker<= 6)
        {
            if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
            {
                tagTracker++;
                UpdateTagTracker();
                Debug.Log("BEEP2");

            }
        }
        if(tagTracker>6 && tagTracker < 11)
        {
            if(tagTracker%2 == 1)
            {
                if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
                {
                    tagTracker++;
                    UpdateTagTracker();
                    Debug.Log("BEEP");

                }
            }
            else
            {
                if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
                {
                    tagTracker++;
                    UpdateTagTracker();
                    Debug.Log("BEEP2");

                }
            }
        }
        if (tagTracker >= 11)
        {
            if(tagTracker == 13)
            {
                //start the game dude
                if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
                {

                    UpdateTagTracker();
                    Debug.Log("Game Start");
                }
            }
            if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
            {
                tagTracker++;
                UpdateTagTracker();
                Debug.Log("BEEP");

            }
        }
       
    }
    void UpdateTagTracker()
    {
        foreach (Selectable s in elements)
        {
            if (s.tag == "Loadout Menu" && tagTracker == 13)
            {
                Debug.Log("Bippity boppity boo");
                s.interactable = true;
                s.Select();
                ExecuteEvents.Execute(startButton.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler); 
                
            }
                if (s.tag == ""+ tagTracker)
            {
                
                s.interactable = true;
                currentActiveElement = s;
            }
            else
            {
                s.interactable = false;
            }

        }
    }
}
