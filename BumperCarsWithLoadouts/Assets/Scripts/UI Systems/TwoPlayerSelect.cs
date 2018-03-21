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
    public List<Selectable> elements;
    private Selectable currentActiveElement;
    private int tagTracker;
    private bool istheDAMNtriggerinuse = false;
    private bool selected = false;
    private bool horizontalInUse = false;
    private bool verticalInUse = false;
    public Selectable selectMan;
    public MyEventSystem currentEventSystem;
    public Text weaponDesc;
    public Text weightDesc;


    // Use this for initialization
    void Start()
    {
        
        tagTracker = 9;
        UpdateTagTracker();
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleManager();
        SettingEditor();
        TextEditor();
    }
    void ToggleManager()
    {

        if (Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.A))
        {
            selected = !selected;
            if (selected)
            {
                currentActiveElement.Select();
                
            }
            else
            {
                currentEventSystem.SetSelectedGameObject(selectMan.gameObject);
            }

        }
        if (Input.GetButtonDown("Cancel2") || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (selected)
            {
                selected = !selected;
            }
        }
        if (Input.GetAxisRaw("Vertical_UI2") != 0)
        {

            if (verticalInUse == false)
            {
                if (!selected)
                {
                    if (Input.GetAxisRaw("Vertical_UI2") > 0)
                    {
                        if (tagTracker < 12 )
                        {
                            tagTracker++;
                            UpdateTagTracker();
                        }
                    }
                    else if (Input.GetAxisRaw("Vertical_UI2") < 0)
                    {

                        if (tagTracker < 12 && tagTracker > 9)
                        {
                            tagTracker--;
                            UpdateTagTracker();
                        }
                    }
                }
            }
            verticalInUse = true;
        }
        else if (Input.GetAxisRaw("Vertical_UI2") == 0)
        {
            verticalInUse = false;
        }

        if (Input.GetAxisRaw("Horizontal_UI2") != 0)
        {

            if (horizontalInUse == false)
            {
                if (Input.GetAxisRaw("Horizontal_UI2") > 0)
                {

                    if (!selected)
                    {
                        if (tagTracker < 12)
                        {

                            tagTracker = 12;
                            UpdateTagTracker();
                        }
                        else
                        {
                            tagTracker++;
                            UpdateTagTracker();
                        }
                    }
                }
                else if (Input.GetAxisRaw("Horizontal_UI2") < 0)
                {
                    if (!selected)
                    {

                        if (tagTracker > 12)
                        {
                            tagTracker--;
                            UpdateTagTracker();
                        }
                        else if (tagTracker == 12)
                        {
                            tagTracker = 9;
                            UpdateTagTracker();
                        }
                    }
                }
            }

            horizontalInUse = true;
        }
        else
        {
            horizontalInUse = false;
        }

    }
    void TextEditor()
    {
        switch (GameObject.Find("Size-Bottom").GetComponent<MyDropdown>().value)
        {
            case 0:
                weightDesc.text = "More agile, but easier to push around";
                break;
            case 1:
                weightDesc.text = "An average weight. Beginner friendly.";
                break;
            case 2:
                weightDesc.text = "Harder to bump, but more dificult to maneuver";
                break;
        }
        switch (GameObject.Find("Weapon-Bottom").GetComponent<MyDropdown>().value)
        {
            case 0:
                weaponDesc.text = "Drop bombs that explode after a short time, or on impact";
                break;
            case 1:
                weaponDesc.text = "Shoot a small, but fast-moving projectile";
                break;
            case 2:
                weaponDesc.text = "Drop paint bombs that temporarily blind your opponent on impact";
                break;
            case 3:
                weaponDesc.text = "Fire a slow-moving homing missile";
                break;
        }


    }
    void SettingEditor()
    {
        if (selected)
        {
            if (tagTracker < 12)
            {
                Slider s = currentActiveElement.GetComponent<MySlider>();
                if (Input.GetAxis("Vertical_UI2") > .2 || Input.GetAxis("Vertical_UI2") < -.2)
                {
                    float slideInput = Input.GetAxis("Vertical_UI2");
                    s.value += slideInput / 10;

                }
                else if (Input.GetAxis("Horizontal_P2") > .2 || Input.GetAxis("Horizontal_P2") < -.2)
                {
                    float slideInput = Input.GetAxis("Horizontal_P2");
                    s.value += slideInput / 10;

                }
            }
            else if (tagTracker >= 12)
            {
                MyDropdown d = currentActiveElement.GetComponent<MyDropdown>();
                float slideInput = Input.GetAxis("Vertical_UI2");
                
                if (Input.GetAxisRaw("Vertical_UI2") != 0)
                {
                    if (istheDAMNtriggerinuse == false)
                    {
                        if (Input.GetAxisRaw("Vertical_UI2") < 0)
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
                        else
                        {
                            if (d.value > 0)
                            {
                                d.value--;
                                d.RefreshShownValue();
                            }
                            else
                            {
                                d.value = d.options.Count - 1;
                                d.RefreshShownValue();
                            }
                            istheDAMNtriggerinuse = true;
                        }

                    }
                }
                if (Input.GetAxisRaw("Vertical_UI2") == 0)
                {
                    istheDAMNtriggerinuse = false;
                }               

            }
        }      
        
        
       

    }

    void UpdateTagTracker()
    {
        if (!selected)
        {
            foreach (Selectable s in elements)
            {
                s.interactable = false;
            }

        }
        if (tagTracker > 13)
        {
            tagTracker = 13;
        }
        if(tagTracker < 9)
        {
            tagTracker = 9;
        }
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
