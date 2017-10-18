using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAbility : MonoBehaviour {

    // drag in buttons to edit the equipment of the car
    public Dropdown topDropdown;
    public Dropdown bottomDropdown;

    // Use this for initialization
    void Start (){
		
	}
	
	// Update is called once per frame
	void Update (){
        // switch statement for player 1 selection
        switch (topDropdown.captionText.text)
        {
            case "Plow": // activate the plow
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Plow").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon1", "Plow");
                break;

            case "Cannon": // inactivate the plow
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Plow").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Cannon").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon1", "Cannon");
                break;
        }

        // switch statement for player 2 selection
        switch (bottomDropdown.captionText.text)
        {
            case "Plow":
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Plow").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon2", "Plow");
                break;

            case "Cannon":
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Plow").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Cannon").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon2", "Cannon");
                break;
        }
    }
}
