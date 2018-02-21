using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up the nodes for the camera's path around the arena as long as it is circular in nature
/// </summary>
public class CircularCameraView : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        int count = transform.childCount;
        float arenaRad = GameObject.Find("SceneManager").GetComponent<CarManager>().ArenaRadius;

        for (int i = 0; i < count; ++i)
        {
            float xPos = arenaRad * Mathf.Cos(2 * Mathf.PI * i / count) * 1.2f;
            float zPos = arenaRad * Mathf.Sin(2 * Mathf.PI * i / count) * 1.2f;
            //set the node on the camera path to this position
            transform.GetChild(i).position = new Vector3(xPos, transform.GetChild(i).position.y, zPos);
        }

    }
}
