using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public float scrollSpeed;

	Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;		
	}
	
	// Update is called once per frame
	void Update () {
		
		float movement = Mathf.Repeat (Time.time * scrollSpeed, transform.localScale.y);
		transform.position = startPosition + Vector3.forward * movement;
		
	}
}
