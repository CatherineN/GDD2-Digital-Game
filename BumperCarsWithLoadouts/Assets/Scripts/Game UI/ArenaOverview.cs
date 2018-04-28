using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaOverview : MonoBehaviour {

    public GameObject[] path;//holds the point that make up the camera's path around the arena
    public GameObject cam; //camera that moves along that path

    public GameObject topCam;
    public GameObject botCam;
    public GameObject topCanvas;
    public GameObject botCanvas;
    public GameObject introCanvas;
    private BumperPhysics p1;
    private BumperPhysics p2;

    public float rotationDamping = 1.0f;
    public float positionDamping = .5f;
    private Transform target;
    private Vector3 start;//where the start point of the lerping between points in the path is
    private Quaternion rotStart;//where the start point of the lerping between roations
    private int index;//index of the path that the camera is seeking
    private bool countdown;//whether the countdown is active or not
    private bool isStarted;//whether the overview and countdown for the current match are over

    private int numPlayers = 2;//how many people are playing
    private int votesToSkip = 0;//how many people want to skip
    private bool p1Voted = false;
    private bool p2Voted = false;

    private CarManager cM;

    static float timer = 0;
    private float percentage = 0;

    public void Awake()
    {
        if(PlayerPrefs.GetString("previousScene") == "GameOver")
        {
            SkipToCountdown();
            countdown = true;
        }

        cM = GameObject.Find("SceneManager").GetComponent<CarManager>();
    }

    // Use this for initialization
    void Start () {
        index = 0;
        countdown = false;
        isStarted = false;
        //everything starts out disabled
        p1 = GameObject.Find("PlayerCar").GetComponent<BumperPhysics>();
        p2 = GameObject.Find("PlayerCar2").GetComponent<BumperPhysics>();

        p1.enabled = false;
        p2.enabled = false;

        foreach(GameObject c in cM.Cars)
        {
            c.GetComponent<VehicleMovement>().enabled = false;
        }

        start = cam.transform.position;//set original start to the beginning

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
            SkipToCountdown();
            target = path[index].transform;

            if (percentage >= 1)
            {
                rotStart = cam.transform.rotation;
                index++;
                start = cam.transform.position;
                target = path[index].transform;
                timer = 0;
                if (index >= path.Length - 1)
                {
                    countdown = true;
                    GoToCountdown();
                }
            }
            timer += Time.deltaTime;

            float timeToStop = 1.0f;// * Vector3.Magnitude(start-target.position)/50;

            // Set the forward to rotate with time

            //float rotPercentage = Utility.MapValue(timer, 0.0f, timeToStop, 0.0f, 1.0f);
            //cam.transform.rotation = Quaternion.Lerp(rotStart, Quaternion.FromToRotation(cam.transform.forward, transform.position - cam.transform.position), rotPercentage/* * positionDamping*/);

            cam.transform.LookAt(transform);

            percentage = Utility.MapValue(timer, 0.0f, timeToStop, 0.0f, 1.0f);
            cam.transform.position = Vector3.Slerp(start, target.position, percentage/* * positionDamping*/);
            return;
        }

        //start countdown
        gameObject.GetComponent<Timer>().enabled = true;

        //make sure to enable everything once the countdown is complete
        if(gameObject.GetComponent<Timer>().timeLeft <= 0)
        {
            p1.enabled = true;
            p2.enabled = true;
            foreach (GameObject c in cM.Cars)
            {
                c.GetComponent<VehicleMovement>().enabled = true;
            }

            if (SceneManager.GetActiveScene().name == "VolcanoArena")
            {
                GameObject.Find("Arena").GetComponent<VolcanoManager>().enabled = true;
            }

            if(gameObject.GetComponent<Timer>().timeLeft <= -1)
            //disable the timer on the canvas to avoid distracting the players
            for (int i = 0; i < gameObject.GetComponent<Timer>().timerLabels.Length; i++)
            {
                gameObject.GetComponent<Timer>().timerLabels[i].enabled = false;
            }
        }



    }

    private void SkipToCountdown()
    {
        //how many players to make majority vote
        int numToSkip = (int)Mathf.Ceil(.66f * numPlayers);

        if ((Input.GetButtonDown("Honk1") || Input.GetKeyDown(KeyCode.Space))&& !p1Voted)
        {
            votesToSkip++;
            p1Voted = true;
        }
        if ((Input.GetButtonDown("Honk2") || Input.GetKeyDown(KeyCode.Return)) && !p2Voted)
        {
            votesToSkip++;
            p2Voted = true;
        }

        if (votesToSkip >= numToSkip)
        {
            countdown = true;
            GoToCountdown();
        }

        introCanvas.GetComponentInChildren<Text>().text = "Press A to Skip Intro " + votesToSkip + "/" + numToSkip;
    }

    private void GoToCountdown()
    {
        //once the cam reaches the end of the path switch to the two individual cams
        topCam.SetActive(true);
        botCam.SetActive(true);
        GameObject.Find("ArenaCamera").SetActive(false);
        topCanvas.SetActive(true);
        botCanvas.SetActive(true);
        introCanvas.SetActive(false);
    }
}
