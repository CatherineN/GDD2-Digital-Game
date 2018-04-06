using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintBang : MonoBehaviour {

    // Use this for initialization
    private float speed;
    public Vector3 velocity;
    public Vector3 direction;
    public Color parColor;
    private float radius;
    private float fadeTime;
    private bool collapsingArena = false;

    
    void Start ()
    {
        gameObject.GetComponent<Renderer>().material.color = parColor;
        speed = 8.5f;
        radius = GameObject.Find("SceneManager").GetComponent<CarManager>().ArenaRadius;
        fadeTime = 5f;
        if (SceneManager.GetActiveScene().name == "CollapsingArena") collapsingArena = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        radius = GameObject.Find("SceneManager").GetComponent<CarManager>().ArenaRadius;
        velocity = direction * speed;
        transform.forward = direction;
        transform.position += velocity * Time.deltaTime;
        if(transform.position.y < 0 && (collapsingArena ? Physics.Raycast(transform.position, Vector3.down, 2f) : (transform.position.sqrMagnitude < radius * radius)))
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            speed = 0;
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void OnTriggerEnter(Collider car)
    {
        Debug.Log("PAINT");
        /*//If the bomb has not collided with the starting car yet
        if (!dropped)
        {
            //Set droppedBy to the dropping car's playerID, and make sure it doesn't happen again.
            droppedBy = car.gameObject.GetComponent<Player>().playerID;
            dropped = true;
        }
        //If the numbers don't match, explode
        if (droppedBy != car.gameObject.GetComponent<Player>().playerID)
            Explode();*/
        if(car.tag == "Player")
        {
            if (car.GetComponent<BumperPhysics>().playerID == 1 && car.gameObject.GetComponent<Renderer>().materials[1].color != parColor)
            {
                GameObject.Find("top cam").transform.GetChild(0).gameObject.layer = 8;
                for (int i = 0; i < 6; i++)
                {
                    GameObject.Find("top cam").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = parColor;
                    GameObject.Find("top cam").transform.GetChild(0).transform.GetChild(i).gameObject.layer = 8;
                    GameObject.Find("top cam").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<testCoroutine>().StartFade();
                }
                Destroy(gameObject);
            }
            if (car.GetComponent<BumperPhysics>().playerID == 2 && car.gameObject.GetComponent<Renderer>().materials[1].color != parColor)
            {
                GameObject.Find("bot cam").transform.GetChild(0).gameObject.layer = 9;
                for (int i = 0; i < 6; i++)
                {
                    GameObject.Find("bot cam").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = parColor;
                    GameObject.Find("bot cam").transform.GetChild(0).transform.GetChild(i).gameObject.layer = 9;
                    GameObject.Find("bot cam").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<testCoroutine>().StartFade();
                }
                Destroy(gameObject);
            }
        }
        
       
    }

    
}
