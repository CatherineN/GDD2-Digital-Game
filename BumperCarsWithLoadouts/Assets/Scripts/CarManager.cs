using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

    public int numAI; //how many AI cars are spawned in the arena
    public GameObject prefabAI;
    //float to determine the extents of the arena
    public float arenaRadius;

    private List<GameObject> cars;//keeps track of all the cars on the arena
    private bool haveSpawned;//determines if the AI have been spawned in yet

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        cars.Add(GameObject.Find("Player 1"));
        cars.Add(GameObject.Find("Player 2"));
    }

    /// <summary>
    /// spawn specified amount of cars on start equildistant from the edge of the arena
    /// </summary>
    private void SpawnAI()
    {
        for (int i = 0; i < numAI; ++i)
        {
            float xPos = arenaRadius* Mathf.Cos(2 * Mathf.PI * i / numAI);
            float zPos = arenaRadius * Mathf.Sin(2 * Mathf.PI * i / numAI);
            //instantiate car
            GameObject carInstance = Instantiate(prefabAI, new Vector3(xPos, 0, zPos), Quaternion.identity) as GameObject;
            //add car to list
            cars.Add(carInstance);
        }
        haveSpawned = true;
    }
}
