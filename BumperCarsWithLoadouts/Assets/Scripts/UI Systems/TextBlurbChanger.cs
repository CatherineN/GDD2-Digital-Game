using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBlurbChanger : MonoBehaviour {

    public Text[] textBlurbs;
    public MyDropdown[] relatedDropdowns;

    private bool valueChanged = false;
    private int[] currentIndices;
	// Use this for initialization
	void Start () {
		
        //link the indices with their dropdowns by array index
        for(int n = 0; n< relatedDropdowns.Length; n++)
        {
            currentIndices[n] = relatedDropdowns[n].value;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    bool UpdateValues()
    {
        for(int n = 0; n < relatedDropdowns.Length; n++)
        {

        }
        return false;
    }
    void DetectChange()
    {

        
    }
}
