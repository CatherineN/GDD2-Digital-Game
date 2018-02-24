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
        switch (topDropdown.value)
        {
            case 0: // activate the plow
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Bomb Dropper").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Paintbang Launcher").gameObject.SetActive(false);
                PlayerPrefs.SetString("weapon1", "Bomb Dropper");
                break;

            case 1: // inactivate the plow
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Cannon").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Paintbang Launcher").gameObject.SetActive(false);
                PlayerPrefs.SetString("weapon1", "Cannon");
                break;

            case 2: // inactivate the plow
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Paintbang Launcher").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon1", "PaintBang");
                break;

            case 3: // inactivate the plow
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Paintbang Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P1").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon1", "Rocket Punch");
                break;
        }

        // switch statement for player 2 selection
        switch (bottomDropdown.value)
        {
            case 0:
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Bomb Dropper").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Paintbang Launcher").gameObject.SetActive(false);

                PlayerPrefs.SetString("weapon2", "Bomb Dropper");
                break;

            case 1:
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Cannon").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Paintbang Launcher").gameObject.SetActive(false);
                PlayerPrefs.SetString("weapon2", "Cannon");
                break;

            case 2:
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Paintbang Launcher").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon2", "PaintBang");
                break;

            case 3: // inactivate the plow
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Bomb Dropper").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Cannon").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Paintbang Launcher").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("P2").transform.FindChild("Rocket Punch Launcher").gameObject.SetActive(true);
                PlayerPrefs.SetString("weapon2", "Rocket Punch");
                break;
        }
    }
}
