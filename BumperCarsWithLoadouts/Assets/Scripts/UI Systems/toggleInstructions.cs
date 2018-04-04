using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleInstructions : MonoBehaviour {
    bool instructions = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Toggle();
        updateCanvas();
	}
    bool Toggle()
    {
        if(Input.GetButtonDown("Select"))
        {
            Debug.Log("fuck");
            instructions = !instructions;
        }
        return instructions;
    }
    void updateCanvas()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = instructions;
    }
}
