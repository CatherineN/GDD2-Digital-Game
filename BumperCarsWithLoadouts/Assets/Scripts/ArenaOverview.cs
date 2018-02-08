using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaOverview : MonoBehaviour {

    public GameObject[] path;//holds the point that make up the camera's path around the arena
    public GameObject cam; //camera that moves along that path

    public GameObject topCam;
    public GameObject botCam;
    public GameObject topCanvas;
    public GameObject botCanvas;
    private Player p1;
    private Player p2;

    public float rotationDamping = 1.0f;
    public float positionDamping = .5f;
    private Transform target;
    private int index;
    private bool countdown;//whether the countdown is active or not
    private bool isStarted;//whether the overview and countdown for the current match are over

    // Use this for initialization
    void Start () {
        index = 0;
        countdown = false;
        isStarted = false;
        //everything starts out disabled
        p1 = GameObject.Find("PlayerCar").GetComponent<Player>();
        p2 = GameObject.Find("PlayerCar2").GetComponent<Player>();

        p1.enabled = false;
        p2.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //don't execute if the match has started
        if (isStarted)
            return;

        //move the camera through the points at a reasonable speed

        //get the current target
        if (!countdown)
        {
            target = path[index].transform;

            if (Vector3.Distance(cam.transform.position, target.position) < 5)
            {
                index++;
                Debug.LogWarning(index);
                if (index >= path.Length - 1)
                {
                    countdown = true;
                    //once the cam reaches the end of the path switch to the two individual cams
                    topCam.SetActive(true);
                    botCam.SetActive(true);
                    GameObject.Find("ArenaOverview").SetActive(false);
                    topCanvas.SetActive(true);
                    botCanvas.SetActive(true);
                }
            }

            // Set the forward to rotate with time
            cam.transform.LookAt(transform);
            //cam.transform.forward = Vector3.Lerp(cam.transform.forward, target.forward, Time.deltaTime * rotationDamping);
            cam.transform.position = Vector3.Lerp(cam.transform.position, target.position, Time.deltaTime * positionDamping);
            return;
        }

        //start countdown
        gameObject.GetComponent<Timer>().enabled = true;

        //make sure to enable everything once the countdown is complete
        if(gameObject.GetComponent<Timer>().timeLeft <= 0)
        {
            p1.enabled = true;
            p2.enabled = true;

            if(gameObject.GetComponent<Timer>().timeLeft <= -1)
            //disable the timer on the canvas to avoid distracting the players
            for (int i = 0; i < gameObject.GetComponent<Timer>().timerLabels.Length; i++)
            {
                gameObject.GetComponent<Timer>().timerLabels[i].enabled = false;
            }
        }



    }
}
