using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaOverview : MonoBehaviour {

    public GameObject[] path;//holds the point that make up the camera's path around the arena
    public Camera cam; //camera that moves along that path

    public float rotationDamping = 1.0f;
    public float positionDamping = .5f;
    private Transform target;
    private int index;
    private bool countdown;//whether the countdown is active or not

    // Use this for initialization
    void Start () {
        index = 0;
        countdown = false;
		//everything starts out disabled
	}
	
	// Update is called once per frame
	void Update () {
        //move the camera through the points at a reasonable speed

        //get the current target
        if(!countdown)
        {
            target = path[index].transform;

            if (Vector3.Distance(cam.transform.position, target.position) < .5f)
            {
                index++;
                Debug.LogWarning(index);
                if (index >= path.Length - 1)
                {
                    countdown = true;
                }
            }

            // Set the forward to rotate with time
            cam.transform.forward = Vector3.Lerp(cam.transform.forward, target.forward, Time.deltaTime * rotationDamping);
            cam.transform.position = Vector3.Lerp(cam.transform.position, target.position, Time.deltaTime * positionDamping);
            return;
        }        

        //once the cam reaches the end of the path switch to the two individual cams

        //start countdown

        //make sure to enable everything once the countdown is complete

        //disable the timer on the canvas to avoid distracting the players
    }
}
