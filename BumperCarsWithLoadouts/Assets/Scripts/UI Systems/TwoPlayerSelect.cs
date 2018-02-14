using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TwoPlayerSelect : MonoBehaviour
{

    public Canvas menuSelect;
    public MyButton startButton;
    private bool p1Active;
    private List<Selectable> elements;
    private Selectable currentActiveElement;
    private int tagTracker;
    private bool istheDAMNtriggerinuse = false;

    // Use this for initialization
    void Start()
    {
        elements = new List<Selectable>();
        elements = Selectable.allSelectables;
        tagTracker = 9;
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

        if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
        {
            if(tagTracker < 13)
            tagTracker++;
            UpdateTagTracker();

        }
        if (Input.GetButtonDown("Cancel2") || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (tagTracker > 9)
            {
                tagTracker--;
                UpdateTagTracker();

            }
        }
    }
    void SettingEditor()
    {        
        if (tagTracker < 12)
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
        
        else if (tagTracker >= 12)
        {
            MyDropdown d = currentActiveElement.GetComponent<MyDropdown>();
            
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

    void UpdateTagTracker()
    {
        foreach (Selectable s in elements)
        {           
            if (s.tag == "" + tagTracker)
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
