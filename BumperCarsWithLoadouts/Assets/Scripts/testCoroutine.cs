using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCoroutine : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Color fadeColor = gameObject.GetComponent<Renderer>().material.color;
        fadeColor.a = 0f;
        gameObject.GetComponent<Renderer>().material.color = fadeColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator test()
    {
        float alpha = 1.0f;
        while (alpha >= 0)
        {
            Color fadeColor = gameObject.GetComponent<Renderer>().material.color;
            fadeColor.a = alpha;
            gameObject.GetComponent<Renderer>().material.color = fadeColor;
            alpha -= Time.deltaTime / 5.0f;
            yield return null;
        }
    }

    public void StartFade()
    {
        Color fadeColor = gameObject.GetComponent<Renderer>().material.color;
        fadeColor.a = 1.0f;
        gameObject.GetComponent<Renderer>().material.color = fadeColor;
        StopAllCoroutines();
        StartCoroutine(test());
    }
}
