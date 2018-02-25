using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAbility : MonoBehaviour
{

    // drag in buttons to edit the equipment of the car
    public Dropdown topDropdown;
    public Dropdown bottomDropdown;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // switch statement for player 1 selection
        switch (topDropdown.value)
        {
            case 0: // activate the bomb dropper
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(0).gameObject.SetActive(false);//Paintbang
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(1).gameObject.SetActive(false);//Cannon
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(2).gameObject.SetActive(true);//bomb
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(3).gameObject.SetActive(false);//Rocket Punch
                PlayerPrefs.SetString("weapon1", "Bomb Dropper");
                break;

            case 1: // activate the cannon
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(0).gameObject.SetActive(false);//Paintbang
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(1).gameObject.SetActive(true);//Cannon
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(2).gameObject.SetActive(false);//bomb
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(3).gameObject.SetActive(false);//Rocket Punch
                PlayerPrefs.SetString("weapon1", "Cannon");
                break;

            case 2: // activate the paintbang
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(0).gameObject.SetActive(true);//Paintbang
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(1).gameObject.SetActive(false);//Cannon
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(2).gameObject.SetActive(false);//bomb
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(3).gameObject.SetActive(false);//Rocket Punch
                PlayerPrefs.SetString("weapon1", "PaintBang");
                break;

            case 3: // activate the rocket punch
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(0).gameObject.SetActive(false);//Paintbang
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(1).gameObject.SetActive(false);//Cannon
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(2).gameObject.SetActive(false);//bomb
                GameObject.FindGameObjectWithTag("P1").transform.GetChild(3).gameObject.SetActive(true);//Rocket Punch
                PlayerPrefs.SetString("weapon1", "Rocket Punch");
                break;
        }

        // switch statement for player 2 selection
        switch (bottomDropdown.value)
        {
            case 0: // activate the bomb dropper
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(0).gameObject.SetActive(false);//Paintbang
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(1).gameObject.SetActive(false);//Cannon
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(2).gameObject.SetActive(true);//bomb
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(3).gameObject.SetActive(false);//Rocket Punch
                PlayerPrefs.SetString("weapon2", "Bomb Dropper");
                break;

            case 1: // activate the cannon
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(0).gameObject.SetActive(false);//Paintbang
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(1).gameObject.SetActive(true);//Cannon
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(2).gameObject.SetActive(false);//bomb
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(3).gameObject.SetActive(false);//Rocket Punch
                PlayerPrefs.SetString("weapon2", "Cannon");
                break;

            case 2: // activate the paintbang
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(0).gameObject.SetActive(true);//Paintbang
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(1).gameObject.SetActive(false);//Cannon
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(2).gameObject.SetActive(false);//bomb
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(3).gameObject.SetActive(false);//Rocket Punch
                PlayerPrefs.SetString("weapon2", "PaintBang");
                break;

            case 3: // activate the rocket punch
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(0).gameObject.SetActive(false);//Paintbang
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(1).gameObject.SetActive(false);//Cannon
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(2).gameObject.SetActive(false);//bomb
                GameObject.FindGameObjectWithTag("P2").transform.GetChild(3).gameObject.SetActive(true);//Rocket Punch
                PlayerPrefs.SetString("weapon2", "Rocket Punch");
                break;
        }
    }
}
