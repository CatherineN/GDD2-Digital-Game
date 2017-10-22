using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarManager : MonoBehaviour {

    public int numAI; //how many AI cars are spawned in the arena
    public GameObject prefabAI;
    //float to determine the extents of the arena
    public float arenaRadius;

    //UI text components of canvas to display
    public Text NumCars1;
    public Text NumCars2;

    private List<GameObject> cars;//keeps track of all the cars on the arena
    private bool haveSpawned;//determines if the AI have been spawned in yet

    private int carsLeft;//how many cars are left in the scene

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetAliveCars();
        UpdateRemainingCarUI();
	}

    //propeties
    public List<GameObject> Cars
    {
        get { return cars; }
        set { cars = value; }
    }

    void Awake()
    {
        //set to false until spawned
        haveSpawned = false;
        //initialize list
        cars = new List<GameObject>();
        if (haveSpawned == false)
        {
            SpawnAI();
        }
        cars.Add(GameObject.Find("PlayerCar"));
        cars.Add(GameObject.Find("PlayerCar2"));
    }

    /// <summary>
    /// spawn specified amount of cars on start equildistant from the edge of the arena
    /// </summary>
    private void SpawnAI()
    {
        for (int i = 0; i < numAI; ++i)
        {
            float xPos = arenaRadius* Mathf.Cos(2 * Mathf.PI * i / numAI)*.9f;
            float zPos = arenaRadius * Mathf.Sin(2 * Mathf.PI * i / numAI) * .9f;
            //instantiate car
            GameObject carInstance = Instantiate(prefabAI, new Vector3(xPos, 0, zPos), Quaternion.identity) as GameObject;
            carInstance.GetComponent<Collision>().stageRadius = arenaRadius;
            //add car to list
            cars.Add(carInstance);
        }
        haveSpawned = true;
    }

    /// <summary>
    /// Determines how many cars are remaining on the arena
    /// Subtracts the amount of dead cars from the total amount of cars in the scene
    /// </summary>
    private void GetAliveCars()
    {
        carsLeft = cars.Count - GameObject.FindGameObjectsWithTag("Dead").Length;
        if (carsLeft == 1)
        {
            PlayerPrefs.SetInt("winner", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerID);
            SceneManager.LoadScene("GameOver");
        }
    }

    /// <summary>
    /// Displays the number of cars left on the arena
    /// </summary>
    private void UpdateRemainingCarUI()
    {
        NumCars1.text = "Cars Remaining: " + carsLeft;
        NumCars2.text = "Cars Remaining: " + carsLeft;
    }
}
