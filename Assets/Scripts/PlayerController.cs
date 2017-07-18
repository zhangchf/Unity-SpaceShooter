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
	public Transform shotSpawnMidPoint;
	public Transform shotSpawnLeftPoint;
	public Transform shotSpawnRightPoint;
	public float fireRate = 0.25f;
	public int fireLevel = 0;


	Rigidbody rb;
	AudioSource audioSrc;
	float nextFireTime = 0f;
	List<Transform> shotSpawnPoints = new List<Transform> ();
	int newFireLevel = 2;

	// After each of this interval, change the fire level.
	int fireLevelChangeTime = 3;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		audioSrc = GetComponent<AudioSource> ();

		InvokeRepeating ("changeFireLevel", 0, fireLevelChangeTime);
	}

	void Update() {

		if (Input.GetButton ("Fire1") && Time.time >= nextFireTime) {
			nextFireTime = Time.time + fireRate;
//			Instantiate (shot, shotSpawnMidPoint.position, shotSpawnMidPoint.rotation);
			if (newFireLevel != fireLevel || shotSpawnPoints.Count == 0) {
				fireLevel = newFireLevel;
				shotSpawnPoints.Clear ();
				switch (fireLevel) {
				case 1:
					shotSpawnPoints.Add (shotSpawnMidPoint);
					break;
				case 2:
					shotSpawnPoints.Add (shotSpawnLeftPoint);
					shotSpawnPoints.Add (shotSpawnRightPoint);
					break;
				case 3:
					shotSpawnPoints.Add (shotSpawnMidPoint);
					shotSpawnPoints.Add (shotSpawnLeftPoint);
					shotSpawnPoints.Add (shotSpawnRightPoint);
					break;
				}
			}
			foreach (Transform t in shotSpawnPoints) {
				Instantiate (shot, t, false);
			}
			audioSrc.Play ();
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

	void changeFireLevel() {
		newFireLevel = (fireLevel) % 3 + 1;
	}
}
