using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScroller : MonoBehaviour {
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Time.deltaTime * scrollSpeed * 100;
        transform.position = transform.position + Vector3.right * newPosition;
        if(transform.position.x > 1000)
        {
            transform.position = new Vector3 (-944 + (transform.position.x-1000), transform.position.y, transform.position.z);
        }
    }
}
