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


	//Touch movement and fire Handling
	public TouchZone touchZone;
	public FireZone leftFireZone;
	public FireZone rightFireZone;
	public Vector3 touchTargetMovement;


	Rigidbody rb;
	AudioSource audioSrc;
	float nextFireTime = 0f;
	List<Transform> shotSpawnPoints = new List<Transform> ();
	int newFireLevel = 2;

	// After each of this interval, change the fire level.
	int fireLevelChangeTime = 3;

	//Accelerometer Handling
	Quaternion calibrationQuaternion;

	// Tou


	void Start() {
		rb = GetComponent<Rigidbody> ();
		audioSrc = GetComponent<AudioSource> ();

		InvokeRepeating ("changeFireLevel", 0, fireLevelChangeTime);

//		CalibrateAccelerometer ();
	}

	void Update() {
//		Debug.Log ("leftCanFire=" + leftFireZone.CanFire () + ",rightCanFire=" + rightFireZone.CanFire ());
		bool canFire = leftFireZone.CanFire () || rightFireZone.CanFire ();
		if (/*Input.GetButton ("Fire1")*/ canFire && Time.time >= nextFireTime) {
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

		// Keyboard control movement.
		if (Input.GetAxis ("Horizontal") != 0f || Input.GetAxis ("Vertical") != 0f) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
			Debug.Log ("keyboard movement=" + movement);
			rb.velocity = movement * speed;
		}  else if (touchZone.GetDirection ().magnitude != 0f) {
			// Touch control movement. 
			Vector2 direction = touchZone.GetDirection ();
			Vector3 movement = new Vector3 (direction.x, 0f, direction.y);
			rb.velocity = movement * speed / 1.2f;
		} /* else if (touchTargetMovement.magnitude != 0f) {
			Vector3 touchMovement = Vector3.MoveTowards (transform.position, touchTargetMovement, Time.deltaTime*10f);
			if (touchMovement.magnitude == 0f) {
				touchTargetMovement = Vector3.zero;
			}
			rb.MovePosition (touchMovement);
		} */


		// Accelerometer control movement.
//		Vector3 accelerationRaw = Input.acceleration;
//		Vector3 acceleration = FixAcceleration (accelerationRaw);
//		Vector3 movement = new Vector3 (acceleration.x, 0f, acceleration.y);


		float clampedX = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax);
		float clampedZ = Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax);
		rb.position = new Vector3 (clampedX, 0.0f, clampedZ);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	void changeFireLevel() {
		newFireLevel = (fireLevel) % 3 + 1;
	}

	void CalibrateAccelerometer() {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0f, 0f, -1f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration(Vector3 acceleration) {
		return calibrationQuaternion * acceleration;
	}

	public void MoveByTouch(Vector3 movementRaw) {
		touchTargetMovement = transform.position + movementRaw;
	}
}
