using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Explosion : MonoBehaviour
{
    public float maxRadius = 100.0f;
    private float radius;
    private float alpha;
    private Material mat;
    private Vector3 randDir;
	// Use this for initialization
	void Start ()
    {
        radius = 1.0f;
        alpha = 1.0f;
        mat = gameObject.GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(radius < maxRadius)
        {
            radius += 2.0f;
            gameObject.transform.localScale = new Vector3(radius, radius, radius);
        }
        else
        {
            alpha -= 0.05f;
            if (alpha <= 0)
                Destroy(gameObject);
            mat.SetFloat("_Transparency", alpha);
        }
        transform.Rotate(transform.up, 2.5f);
	}

    public void SetRadius(float rad)
    {
        maxRadius = rad / 10;
        StartCoroutine(Vibrate(0.15f, 3f, 0));
        StartCoroutine(Vibrate(0.15f, 3f, PlayerIndex.Two));
    }
    IEnumerator Vibrate(float length, float intensity, PlayerIndex index)
    {
        GamePad.SetVibration(index, intensity, intensity);
        yield return new WaitForSeconds(length);
        GamePad.SetVibration(index, 0, 0);
    }
}
