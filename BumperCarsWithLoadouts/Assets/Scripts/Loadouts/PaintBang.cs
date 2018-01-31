using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBang : MonoBehaviour {

    // Use this for initialization
    private float speed;
    public Vector3 velocity;
    public Vector3 direction;
    public Color parColor;
    private float radius;
    private float fadeTime;
    void Start ()
    {
        gameObject.GetComponent<Renderer>().material.color = parColor;
        speed = 8.5f;
        radius = GameObject.Find("PlayerCar").GetComponent<Collision>().StageRadius;
        fadeTime = 5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        velocity = direction * speed;
        transform.forward = direction;
        transform.position += velocity * Time.deltaTime;
        if(transform.position.y < 0 && transform.position.sqrMagnitude < radius * radius)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            speed = 0;
        }
    }

    public void OnTriggerEnter(Collider car)
    {
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
            if (car.GetComponent<Player>().playerID == 1 && car.gameObject.GetComponent<Renderer>().materials[1].color != parColor)
            {
                GameObject.Find("top cam").transform.GetChild(0).gameObject.SetActive(true);
                for(int i = 0; i < 6; i++)
                {
                    GameObject.Find("top cam").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = parColor;
                    //Debug.Log("Hi");
                    StartCoroutine(paintFade(GameObject.Find("top cam").transform.GetChild(0).transform.GetChild(i).gameObject));
                }
                Destroy(gameObject);
            }
            if (car.GetComponent<Player>().playerID == 2 && car.gameObject.GetComponent<Renderer>().materials[1].color != parColor)
            {
                GameObject.Find("bot cam").transform.GetChild(0).gameObject.SetActive(true);
                for (int i = 0; i < 6; i++)
                {
                    GameObject.Find("bot cam").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = parColor;
                    //Debug.Log("Hi");
                    StartCoroutine(paintFade(GameObject.Find("bot cam").transform.GetChild(0).transform.GetChild(i).gameObject));
                }
                Destroy(gameObject);
            }
        }
        
       
    }
    IEnumerator paintFade(GameObject go)
    {
        float time = 0;
        float alpha = 1f;
        fadeTime = 5f;
        Color fadeColor = go.GetComponent<Renderer>().material.color;
        while(time < fadeTime)
        {
            time += Time.deltaTime;
            alpha -= 0.2f;
            Debug.Log(time);
            go.GetComponent<Renderer>().material.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return null;
        }
        
    }
}
