using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour {

    public int num;//the number that is associated with this node
    private CarManager cM;
    private float ogArena;
    private Vector3 ogPos;

    // Use this for initialization
    void Start () {
        cM = GameObject.Find("SceneManager").GetComponent<CarManager>();
        ogArena = cM.ArenaRadius;
        ogPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name == "DefaultArena")
        {
            transform.position = (ogPos) * (cM.ArenaRadius / ogArena);
            transform.position = new Vector3(transform.position.x, .1f, transform.position.z);
        }
	}
}
