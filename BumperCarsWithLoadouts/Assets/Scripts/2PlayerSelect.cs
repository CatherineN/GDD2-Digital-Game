using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    public Canvas menuSelect;
    private bool p1Active = true;
    private List<Selectable> elements;
    private Selectable currentActiveElement;

	// Use this for initialization
	void Start () {
        elements = Selectable.allSelectables;
        foreach (Selectable s in elements)
        {
            s.interactable = false;
        }
        elements[0].interactable = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
