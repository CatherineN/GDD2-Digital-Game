using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Authors: Kat Weis, Cameron Schlesinger & Erin Casicoli
/// Purpose: To have a camera smoothly follow the target around
/// </summary>

// This camera smoothes out rotation around the y-axis and height.
// Horizontal Distance to the target is always fixed.
// For every one of those smoothed values, calculate the wanted value and the current value.
// Smooth it using the Lerp function and apply the smoothed values to the transform's position.
public class SmoothFollow : MonoBehaviour
{
    //fields
    public Transform target;
    public float distance = 3.0f;
    public float height = 1.50f;
    public float heightDamping = 2.0f;
    public float positionDamping = 2.0f;
    public float rotationDamping = 2.0f;
    public bool lookBehind;
    public bool isPlayerOne;
    public bool isDead = false;

    //the guy to look at after death
    public GameObject friend;

    //toggle to switch from 3rd to 1st person, pretty easy
    public bool firstPerson = false;

    private Vector3 defaultPos = new Vector3(0, 24.5f, -63);
    private bool wideSpec = true;
    //private Quaternion defaultRot = new Quaternion(23.992f, 0, 0, 0);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        ListenForToggle();
        ListenForDeath();
        if (ListenForGameOver())
        {
            PlayerPrefs.SetInt("winner", 3);
            SceneManager.LoadScene("GameOver");
        }
    }
    void LateUpdate()
    {
        if (!isDead) //check if it's still alive first
        {
            if (firstPerson)
            {
                positionDamping = 15f;
            }

            // Early exit if there’s no target
            if (!target) return;
            float wantedHeight = target.position.y + height;
            float currentHeight = transform.position.y;
            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            // Set the position of the camera 

            Vector3 wantedPosition = target.position;
            if (!firstPerson)
            {
                wantedPosition = target.position - target.forward * distance;
            }

            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * positionDamping);
            // Adjust the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            if (lookBehind == true)
            {
                // Set the forward to rotate with time backwards if it is the background facing one
                transform.forward = Vector3.Lerp(transform.forward, -target.forward, Time.deltaTime * rotationDamping);
            }
            else
            {
                // Set the forward to rotate with time
                transform.forward = Vector3.Lerp(transform.forward, target.forward, Time.deltaTime * rotationDamping);
            }
        }
        else if (wideSpec)
        {
            gameObject.transform.position = defaultPos;
            gameObject.transform.LookAt(friend.transform);
            
        }
        else
        {
            // Early exit if there’s no target
            if (!friend) return;
            float wantedHeight = friend.transform.position.y + height;
            float currentHeight = friend.transform.position.y;


            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);


            // Set the position of the camera 
            Vector3 wantedPosition = friend.transform.position - friend.transform.forward * distance;            
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * positionDamping);


            // Adjust the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            if (lookBehind == true)
            {
                // Set the forward to rotate with time backwards if it is the background facing one
                transform.forward = Vector3.Lerp(transform.forward, -friend.transform.forward, Time.deltaTime * rotationDamping);
            }
            else
            {
                // Set the forward to rotate with time
                transform.forward = Vector3.Lerp(transform.forward, friend.transform.forward, Time.deltaTime * rotationDamping);
            }
        }

    }
    void ListenForToggle()
    {
        if (!isDead) //check if it's still alive first
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("ToggleCamera1")) && isPlayerOne) //toggle first person with Q if player 1
            {
                if (firstPerson)
                {
                    firstPerson = false;
                }
                else
                {
                    firstPerson = true;
                }
            }
            else if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetButtonDown("ToggleCamera2")) && !isPlayerOne) // toggle first person with numpad 0 if player 2
            {
                if (firstPerson)
                {
                    firstPerson = false;
                }
                else
                {
                    firstPerson = true;
                }
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("ToggleCamera1")) && isPlayerOne) //toggle spectator mode with Q if player 1
            {
                if (wideSpec)
                {
                    wideSpec = false;
                }
                else
                {
                    wideSpec = true;
                }
            }
            else if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("ToggleCamera2")) && !isPlayerOne) // toggle spectator mode with numpad 0 if player 2
            {
                if (wideSpec)
                {
                    wideSpec = false;
                }
                else
                {
                    wideSpec = true;
                }
            }
        }

    }
    void ListenForDeath()
    {
        if (gameObject.transform.position.y < -10)
        {
            isDead = true;
            target.gameObject.tag = "Dead";
        }
    }
    bool ListenForGameOver()
    {
        if (isDead)
        {
            if(friend.transform.position.y < -10)
            {
                return true;
            }
        }
        return false;
        
    }
}
