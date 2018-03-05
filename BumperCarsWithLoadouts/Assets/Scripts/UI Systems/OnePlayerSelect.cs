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
    private bool horizontalInUse = false;
    private bool verticalInUse = false;
    public Selectable selectMan;
    

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
            selected = !selected;
            if (selected)
            {
                currentActiveElement.Select();
                elements[8].interactable = false;
            }
            else
            {                
                currentEventSystem.SetSelectedGameObject(selectMan.gameObject);                                
            }
            if(tagTracker == 8)
            {                                
                var pointer = new PointerEventData(currentEventSystem);
                ExecuteEvents.Execute(startButton.gameObject, pointer, ExecuteEvents.submitHandler);
                startTheDamnGamePlease.Load();
            }

        }
        if (Input.GetButtonDown("Cancel1") || Input.GetKeyDown(KeyCode.Space))
        {
            if (selected)
            {
                selected = !selected;
            }
            
        }
        if (Input.GetAxisRaw("Vertical_UI1") != 0)
        {
           
            if (verticalInUse == false)
            {                
                if (!selected)
                {
                    if (Input.GetAxisRaw("Vertical_UI1") > 0)
                    {
                        if (tagTracker < 4 && tagTracker > 1)
                        {
                            tagTracker--;
                            UpdateTagTracker();
                        }
                        else if (tagTracker == 6)
                        {
                            tagTracker = 4;
                            UpdateTagTracker();
                        }
                        else if (tagTracker == 7)
                        {
                            tagTracker = 5;
                            UpdateTagTracker();
                        }                        
                    }
                    else if (Input.GetAxisRaw("Vertical_UI1") < 0)
                    {                     
                        
                        if (tagTracker < 4)
                        {
                            tagTracker++;
                            UpdateTagTracker();
                        }
                        else if (tagTracker == 4)
                        {
                            tagTracker = 6;
                            UpdateTagTracker();
                        }
                        else if (tagTracker == 5)
                        {
                            tagTracker = 7;
                            UpdateTagTracker();
                        }                       
                    }
                }            
            }
            verticalInUse = true;
        }
        else if (Input.GetAxisRaw("Vertical_UI1") == 0)
        {
            verticalInUse = false;
        }

        if (Input.GetAxisRaw("Horizontal_UI1") !=0) {
           
            if (horizontalInUse == false)
            {
                if (Input.GetAxisRaw("Horizontal_UI1") > 0)
                {
                    
                    if (!selected)
                    {
                        if (tagTracker < 4)
                        {

                            tagTracker = 4;
                            UpdateTagTracker();
                        }
                        else
                        {
                            tagTracker++;
                            UpdateTagTracker();
                        }
                    }
                }
                else if (Input.GetAxisRaw("Horizontal_UI1") < 0)
                {
                    if (!selected)
                    {

                        if (tagTracker > 4)
                        {
                            tagTracker--;
                            UpdateTagTracker();
                        }
                        else if (tagTracker == 4)
                        {
                            tagTracker = 1;
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
    void SettingEditor()
    {

        if (tagTracker < 4)
        {
            if (selected)
            {
                Slider s = currentActiveElement.GetComponent<MySlider>();
                if (Input.GetAxis("Vertical_UI1") > .2 || Input.GetAxis("Vertical_UI1") < -.2)
                {
                    float slideInput = Input.GetAxis("Vertical_UI1");
                    s.value += slideInput / 10;

                }
                else if (Input.GetAxis("Horizontal_P1") > .2 || Input.GetAxis("Horizontal_P1") < -.2)
                {
                    float slideInput = Input.GetAxis("Horizontal_P1");
                    s.value += slideInput / 10;

                }
            }
            

        }        
        else if (tagTracker < 8)
        {
            if (selected)
            {
                MyDropdown d = currentActiveElement.GetComponent<MyDropdown>();
                float slideInput = Input.GetAxis("Vertical_UI1");

                if (Input.GetAxisRaw("Vertical_UI1") != 0)
                {
                    if (istheDAMNtriggerinuse == false)
                    {
                        if (Input.GetAxisRaw("Vertical_UI1") < 0)
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
                if (Input.GetAxisRaw("Vertical_UI1") == 0)
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
        foreach (Selectable s in elements)
        {
            
            if (tagTracker == 8)
            {
                elements[7].interactable = true;
                currentActiveElement = elements[7];
            }
            if (s.tag == "" + tagTracker)
            {                
               
                s.interactable = true;
                
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
