using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour {

	public float speed = 20.0f;

	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}

}
