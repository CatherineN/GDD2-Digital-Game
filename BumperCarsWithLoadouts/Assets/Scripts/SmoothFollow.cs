using UnityEngine;
using System.Collections;
/// <summary>
/// Authors: Kat Weis & Erin Casicoli
/// Purpose: To have a camera smoothly follow the average position of the flock around
/// Bugs: The camera can go inside the terrain while the flockers are not inside the terrain making it look like it is glitching
/// </summary>

// This camera smoothes out rotation around the y-axis and height.
// Horizontal Distance to the target is always fixed.
// Forevery one of those smoothed values, calculate the wanted value and the current value.
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

    //toggle to switch from 3rd to 1st person, pretty easy
    public bool firstPerson = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        ListenForToggle();
    }
    void LateUpdate()
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
        transform.position = new Vector3(transform.position.x , currentHeight, transform.position.z);
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
    void ListenForToggle()
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
        else if ((Input.GetKeyDown(KeyCode.Keypad0) || Input.GetButtonDown("ToggleCamera2")) && !isPlayerOne) // toggle first person with numpad 0 if player 2
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

}
