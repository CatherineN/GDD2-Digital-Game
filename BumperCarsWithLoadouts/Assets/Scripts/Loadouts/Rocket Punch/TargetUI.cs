using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour {
    public GameObject target;
    public Camera cam;
    private RectTransform rect;
    private RectTransform parentRect;
	// Use this for initialization
	void Start () {
        rect = gameObject.GetComponent<RectTransform>();
        parentRect = transform.parent.gameObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 viewport = cam.WorldToViewportPoint(target.transform.position);
        Vector2 screenPos = new Vector2((viewport.x * parentRect.sizeDelta.x) - (parentRect.sizeDelta.x * 0.5f), (viewport.y * parentRect.sizeDelta.y) - (parentRect.sizeDelta.y * 0.5f));
        rect.anchoredPosition = screenPos;
        rect.Rotate(0, 0, 2f);
	}
}
