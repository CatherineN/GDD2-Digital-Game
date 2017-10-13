using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // to make this script adaptable, it looks at the tag of the scene, and
        // then loads the appropriate scene.
        if (Input.GetButtonDown("Start"))
        {
            if (gameObject.tag == "Main Menu")
            {
                SceneManager.LoadScene("LoadOuts");
            }
            else if (gameObject.tag == "Loadout Menu")
            {
                SceneManager.LoadScene("Bob");
            }
        }

    }

}
