using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFallingStage : MonoBehaviour
{
    private float xOffset = 0.2136f;
    private float zOffset = 0.2466f;
    private float halfX;
    private float halfZ;
    public int tiles = 60;
    public int maxPerRow = 7;
    public GameObject tilePrefab;

	// Use this for initialization
	void Start ()
    {
        halfX = xOffset / 2.0f;
        halfZ = zOffset / 2.0f;
        MakeStage();
	}
	
	private void MakeStage()
    {
        float currentX = 0;
        float currentZ = 0;
        int counter = 0;
        bool longR = true;
        while(counter < tiles)
        {
            if(longR)
            {
                SpawnLongRow(currentX, currentZ);
                counter += maxPerRow;
                longR = false;
                currentX += xOffset;
                currentZ = halfZ;
                continue;
            }
            else
            {
                SpawnShortRow(currentX, currentZ);
                counter += maxPerRow - 1;
                longR = true;
                currentX += xOffset;
                currentZ = 0f;
                continue;
            }
        }
    }

    private void SpawnLongRow(float x, float z)
    {
        float currentX = x;
        float currentZ = z;
        for (int i = 0; i < maxPerRow; i++)
        {
            Vector3 pos = new Vector3(currentX, -0.1382384f, currentZ);
            GameObject clone = Instantiate(tilePrefab, pos, Quaternion.identity, gameObject.transform);
            clone.transform.localPosition = pos;
            currentZ += zOffset;
        }
    }

    private void SpawnShortRow(float x, float z)
    {
        float currentX = x;
        float currentZ = z;
        for (int i = 0; i < maxPerRow - 1; i++)
        {
            Vector3 pos = new Vector3(currentX, -0.1382384f, currentZ);
            GameObject clone = Instantiate(tilePrefab, pos, Quaternion.identity, gameObject.transform);
            clone.transform.localPosition = pos;
            currentZ += zOffset;
        }
    }
}
