using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroyer : MonoBehaviour {

	public GameObject asteroidExplosion;
	public GameObject playerExplosiion;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate (asteroidExplosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
			Instantiate (playerExplosiion, other.transform.position, other.transform.rotation);
		}
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
