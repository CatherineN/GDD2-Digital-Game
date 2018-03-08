using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    int timer = 0;
    private MeshRenderer rend;
    private Color colorStart;
    private Color colorEnd;
    private float duration;
	// Use this for initialization
	void Start ()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        colorStart = rend.material.color;
        colorEnd = Color.black;
        duration = 0.125f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
    }
}
