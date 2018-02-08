using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TwoPlayerSelect : MonoBehaviour {

    public Canvas menuSelect;
    public MyButton startButton;
    private bool p1Active;
    private List<Selectable> elements;
    private Selectable currentActiveElement;
    private int tagTracker;
    private bool istheDAMNtriggerinuse = false;

	// Use this for initialization
	void Start () {
        elements = new List<Selectable>();
        elements = Selectable.allSelectables;
        tagTracker = 1;
        UpdateTagTracker();
        p1Active = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        ToggleManager();
        SettingEditor();
	}
    void ToggleManager()
    {
        if (tagTracker <= 3)
        {
            if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
            {
                tagTracker++;
                UpdateTagTracker();
                              

            }
        }
        if (tagTracker > 3 && tagTracker<= 6)
        {
            if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
            {
                tagTracker++;
                UpdateTagTracker();
                
                p1Active = false;
            }
        }
        if(tagTracker>6 && tagTracker < 11)
        {
            if(tagTracker%2 == 1)
            {
                if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
                {
                    tagTracker++;
                    UpdateTagTracker();
                    
                    p1Active = true;
                }
            }
            else
            {
                if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
                {
                    tagTracker++;
                    UpdateTagTracker();
                   
                    p1Active = false;
                }
            }
        }
        if (tagTracker >= 11)
        {
            if(tagTracker == 13)
            {
                //start the game dude
                if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
                {
                    UpdateTagTracker();
                   
                }
            }
            if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
            {
                tagTracker++;
                UpdateTagTracker();               
                p1Active = true;
            }
        }
       
    }
    void SettingEditor()
    {
        if(tagTracker < 7)
        {
            if (tagTracker < 4)
            {
                Slider s = currentActiveElement.GetComponent<MySlider>();
                if(Input.GetAxis("Vertical_P1") > .2 || Input.GetAxis("Vertical_P1") < -.2)
                {
                    float slideInput = Input.GetAxis("Vertical_P1");
                    s.value += slideInput / 10;
                    Debug.Log(slideInput);
                }
                else if (Input.GetAxis("Horizontal_P1") > .2 || Input.GetAxis("Horizontal_P1") < -.2)
                {
                    float slideInput = Input.GetAxis("Horizontal_P1");
                    s.value += slideInput / 10;
                    Debug.Log(slideInput);
                }
                
            }
            else
            {
                Slider s = currentActiveElement.GetComponent<MySlider>();
                if (Input.GetAxis("Vertical_P2") > .2 || Input.GetAxis("Vertical_P2") < -.2)
                {
                    float slideInput = Input.GetAxis("Vertical_P2");
                    s.value += slideInput / 10;
                    Debug.Log(slideInput);
                }
                else if (Input.GetAxis("Horizontal_P2") > .2 || Input.GetAxis("Horizontal_P2") < -.2)
                {
                    float slideInput = Input.GetAxis("Horizontal_P2");
                    s.value += slideInput / 10;
                    Debug.Log(slideInput);
                }
            }
        
        }
        else if (tagTracker < 11)
        {
            Dropdown d = currentActiveElement.GetComponent<Dropdown>();

            if(tagTracker % 2 == 1)
            {
                float slideInput = Input.GetAxis("Vertical_P1");

                if (Input.GetAxisRaw("Vertical_P1") != 0)
                {
                    if (istheDAMNtriggerinuse == false)
                    {
                        if (d.value < (d.options.Count - 1))
                        {
                            d.value++;
                            d.RefreshShownValue();
                        }
                        else
                        {
                            d.value = 0;
                            d.RefreshShownValue();
                        }
                        istheDAMNtriggerinuse = true;
                    }
                }
                if (Input.GetAxisRaw("Vertical_P1") == 0)
                {
                    istheDAMNtriggerinuse = false;
                }
            }
            else
            {

                float slideInput = Input.GetAxis("Vertical_P2");

                if (Input.GetAxisRaw("Vertical_P2") != 0)
                {
                    if (istheDAMNtriggerinuse == false)
                    {
                        if (d.value < (d.options.Count - 1))
                        {
                            d.value++;
                            d.RefreshShownValue();
                        }
                        else
                        {
                            d.value = 0;
                            d.RefreshShownValue();
                        }
                        istheDAMNtriggerinuse = true;
                    }
                }
                if (Input.GetAxisRaw("Vertical_P2") == 0)
                {
                    istheDAMNtriggerinuse = false;
                }
            }
        }
        if(tagTracker == 11)
        {
            /* this code should be uncommented after PLAYTEST 1
            Toggle t = currentActiveElement.GetComponent<Toggle>();
            float slideInput = Input.GetAxis("Vertical_P1");

            if (Input.GetAxisRaw("Vertical_P1") != 0)
            {
                if (istheDAMNtriggerinuse == false)
                {
                    if (t.isOn)
                    {
                        t.isOn = false;
                    }
                    else
                    {
                        t.isOn = true;
                    }
                    istheDAMNtriggerinuse = true;
                }
            }
            if (Input.GetAxisRaw("Vertical_P1") == 0)
            {
                istheDAMNtriggerinuse = false;
            }
            */
        }
        if(tagTracker == 12)
        {
            float slideInput = Input.GetAxis("Vertical_P1");
            Dropdown d = currentActiveElement.GetComponent<Dropdown>();
            if (Input.GetAxisRaw("Vertical_P1") != 0)
            {
                if (istheDAMNtriggerinuse == false)
                {
                    if (d.value < (d.options.Count - 1))
                    {
                        d.value++;
                        d.RefreshShownValue();
                    }
                    else
                    {
                        d.value = 0;
                        d.RefreshShownValue();
                    }
                    istheDAMNtriggerinuse = true;
                }
            }
            if (Input.GetAxisRaw("Vertical_P1") == 0)
            {
                istheDAMNtriggerinuse = false;
            }
        }

    }           
    
    void UpdateTagTracker()
    {
        foreach (Selectable s in elements)
        {
            if (s.tag == "Loadout Menu" && tagTracker == 14)
            {
                
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
