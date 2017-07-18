using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public float dodge;
	public float smoothing;
	public float tilt;
	public Boundary boundary;

	Rigidbody rb;
	float targetManeuver;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine (Dodge ());
	}

	IEnumerator Dodge() {
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while(true) {
			targetManeuver = Random.Range (-dodge, dodge);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}

	void FixedUpdate() {
		float velocityX = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (velocityX, 0f, rb.velocity.z);

		float clampedX = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax);
		float clampedZ = Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax);
		rb.position = new Vector3 (clampedX, 0.0f, clampedZ);

		Vector3 rotationAngles = rb.rotation.eulerAngles;
		rb.rotation = Quaternion.Euler (0f, rotationAngles.y, rb.velocity.x * -tilt);
	}


}
