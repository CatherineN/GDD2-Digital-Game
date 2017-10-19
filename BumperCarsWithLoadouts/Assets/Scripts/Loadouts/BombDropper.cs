using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropper : MonoBehaviour {

    // Use this for initialization
    public GameObject bomb;
    void Start()
    {
        //Debug.LogWarning(gameObject.GetComponent<Renderer>().material.color);
        gameObject.GetComponent<Renderer>().material.color = transform.GetComponentInParent<Renderer>().material.color;
        //Debug.LogWarning(gameObject.GetComponentInChildren<MeshRenderer>().gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = transform.parent.GetComponentInParent<Renderer>().material.color;
        //When the E key is pressed...
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire3"))
        {
            //Create a cannonball at the mouth of the cannon. The cannon is rotated, so we use the transform.up
            GameObject bombInstance = Instantiate(bomb, gameObject.transform.position - (transform.up * transform.localScale.y), new Quaternion(0, 0, 0, 0));
        }
    }
}
