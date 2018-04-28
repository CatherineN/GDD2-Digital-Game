using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarManager : MonoBehaviour {

    private int numAI; //how many AI cars are spawned in the arena
    public GameObject prefabAI;
    public GameObject stageHolder;
    public GameObject nodeHolder;
    //float to determine the extents of the arena
    private float arenaRadius;

    public float ArenaRadius { get { return GameObject.Find("Arena").transform.lossyScale.x * 32; } }

    //UI text components of canvas to display
    public Text NumCars1;
    public Text NumCars2;

    public List<GameObject> cars;//keeps track of all the cars on the arena
    private bool haveSpawned = false;//determines if the AI have been spawned in yet

    private int carsLeft;//how many cars are left in the scene

    public bool aiOn = false;//whether or not to use AI

	// Use this for initialization
	void Start () {
        Utility.SetPhysicsSettings();
	}
	
	// Update is called once per frame
	void Update () {
        GetAliveCars();
        if (SceneManager.GetActiveScene().name != "AI_Testing") UpdateRemainingCarUI();

    }

    //propeties
    public List<GameObject> Cars
    {
        get { return cars; }
        set { cars = value; }
    }

    void Awake()
    {
        //arenaRadius = GameObject.Find("Arena").transform.lossyScale.x * 32;
        numAI = PlayerPrefs.GetInt("numAI");//------------------------------------------------------------------set to hard code for now
        //set to false until spawned
        //if (!aiOn)
        //{
        //    haveSpawned = true;//------------------------------------------------------------------set to true for now so AI do not spawn in arenas
        //}

        //initialize list
        cars = new List<GameObject>();
        if (haveSpawned == false)
        {
            Debug.Log(numAI);
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
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");
        List<Vector3> spawnPos = new List<Vector3>();
        foreach (var s in spawns)
        {
            spawnPos.Add(s.transform.position);
        }
        for (int i = 0; i < numAI; ++i)
        {
            Debug.Log(i);
            float xPos = ArenaRadius* Mathf.Cos(2 * Mathf.PI * i / numAI)*.9f;
            float zPos = ArenaRadius * Mathf.Sin(2 * Mathf.PI * i / numAI) * .9f;

            int index = Random.Range(0, 4 - i);

            //instantiate car
            GameObject carInstance = Instantiate(prefabAI, spawnPos[index], Quaternion.identity) as GameObject;

            //set the nodes for stalking around the arena
            for (int k = 0; k < nodeHolder.transform.childCount; k++)
            {
                carInstance.GetComponent<ScawySystem>().nodes[k] = nodeHolder.transform.GetChild(k).gameObject;
            }
            

            //set the arena
            carInstance.GetComponent<BumperPhysics>().stage = stageHolder;

            //add car to list
            cars.Add(carInstance);

            //remove that spawn point from the list
            spawnPos.RemoveAt(index);
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
            PlayerPrefs.SetInt("winner", GameObject.FindGameObjectWithTag("Player").GetComponent<BumperPhysics>().playerID);
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
