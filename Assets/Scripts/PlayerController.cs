using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed = 10f;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawnPoint;
	public float fireRate = 0.25f;


	Rigidbody rb;
	float nextFireTime = 0f;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	void Update() {
		if (Input.GetButton ("Fire1") && Time.time >= nextFireTime) {
			nextFireTime += fireRate;
			Instantiate (shot, shotSpawnPoint.position, shotSpawnPoint.rotation);
//			Instantiate (shot, shotSpawnPoint, false);
		}
	}

	void FixedUpdate() {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
		rb.velocity = movement * speed;

		float clampedX = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax);
		float clampedZ = Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax);
		rb.position = new Vector3 (clampedX, 0.0f, clampedZ);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
