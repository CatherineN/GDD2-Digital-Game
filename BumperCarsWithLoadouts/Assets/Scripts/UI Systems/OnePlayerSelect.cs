using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnePlayerSelect : MonoBehaviour
{

    public Canvas menuSelect;
    public MyButton startButton;
    public MyEventSystem currentEventSystem;
    public StartGame startTheDamnGamePlease;
    private bool p1Active;
    public List<Selectable> elements;
    private Selectable currentActiveElement;
    private int tagTracker;
    private bool istheDAMNtriggerinuse = false;
    private bool selected = false;

    // Use this for initialization
    void Start()
    {
        
        tagTracker = 1;
        UpdateTagTracker();

    }

    // Update is called once per frame
    void Update()
    {
        ToggleManager();
        SettingEditor();
    }
    void ToggleManager()
    {
        if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
        {
            
            tagTracker++;            
            UpdateTagTracker();

        }
        if (Input.GetButtonDown("Cancel1") || Input.GetKeyDown(KeyCode.Space))
        {
            if(tagTracker > 1)
            {
                tagTracker--;
                UpdateTagTracker();

            }
        }
        if (tagTracker == 8)
        {
            //start the game dude
            if (Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.A))
            {
                UpdateTagTracker();

            }
        }

    }
    void SettingEditor()
    {

        if (tagTracker < 4)
        {
            Slider s = currentActiveElement.GetComponent<MySlider>();
            if (Input.GetAxis("Vertical_P1") > .2 || Input.GetAxis("Vertical_P1") < -.2)
            {
                float slideInput = Input.GetAxis("Vertical_P1");
                s.value += slideInput / 10;
               
            }
            else if (Input.GetAxis("Horizontal_P1") > .2 || Input.GetAxis("Horizontal_P1") < -.2)
            {
                float slideInput = Input.GetAxis("Horizontal_P1");
                s.value += slideInput / 10;
                
            }

        }        
        else if (tagTracker < 8)
        {
            MyDropdown d = currentActiveElement.GetComponent<MyDropdown>();
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
    }

    void UpdateTagTracker()
    {
        foreach (Selectable s in elements)
        {
            if (tagTracker == 8)
            {

                s.interactable = true;
                s.Select();
                Debug.Log("gay nerd button doesnt work");
                var pointer = new PointerEventData(currentEventSystem);
                ExecuteEvents.Execute(startButton.gameObject, pointer, ExecuteEvents.submitHandler);
                startTheDamnGamePlease.Load();

            }
            if (s.tag == "" + tagTracker)
            {

                s.interactable = true;
                s.Select();
                currentActiveElement = s;


            }
            else
            {
                s.interactable = false;
            }

            if (tagTracker > 8)
            {
                tagTracker = 8;
            }

        }
    }
}
