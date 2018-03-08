using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFallingStage : MonoBehaviour
{
    private float xOffset = 0.2136f;
    private float zOffset = 0.2466f;
    private float halfX;
    private float halfZ;
    private float yVal;
    public int tiles = 60;
    public int maxPerRow = 7;
    public GameObject tilePrefab;
    private Color color1;
    private Color color2;
    private Color color3;
    private Color color4;
    private Color color5;
    private Color[] colors;

    // Use this for initialization
    void Start ()
    {
        halfX = xOffset / 2.0f;
        halfZ = zOffset / 2.0f;
        yVal = -0.139f;
        MakeColors();
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
            Vector3 pos = new Vector3(currentX, yVal, currentZ);
            GameObject clone = Instantiate(tilePrefab, pos, Quaternion.identity, gameObject.transform);
            clone.transform.localPosition = pos;
            clone.gameObject.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, 5)];
            currentZ += zOffset;
        }
    }

    private void SpawnShortRow(float x, float z)
    {
        float currentX = x;
        float currentZ = z;
        for (int i = 0; i < maxPerRow - 1; i++)
        {
            Vector3 pos = new Vector3(currentX, yVal, currentZ);
            GameObject clone = Instantiate(tilePrefab, pos, Quaternion.identity, gameObject.transform);
            clone.transform.localPosition = pos;
            clone.gameObject.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, 5)];
            currentZ += zOffset;
        }
    }

    private void MakeColors()
    {
        color1 = new Color(233f / 255f, 77f / 255f, 48f / 255f);
        color2 = new Color(242f / 255f, 39f / 255f, 131f / 255f);
        color3 = new Color(249f / 255f, 248f / 255f, 78f / 255f);
        color4 = new Color(5f / 255f, 238f / 255f, 227f / 255f);
        color5 = new Color(255f / 255f, 36f / 255f, 70f / 255f);
        colors = new Color[5];
        colors[0] = color1;
        colors[1] = color2;
        colors[2] = color3;
        colors[3] = color4;
        colors[4] = color5;
    }
}
