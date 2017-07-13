using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject selfExplosion;
	public GameObject playerExplosiion;

	public float scoreValue = 10f;
	public GameScore gameScore;
	public GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameScore = gameControllerObject.GetComponent<GameScore> ();
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameScore == null) {
			Debug.LogError ("Can't locate GameScore object");
		}
		if (gameController == null) {
			Debug.LogError ("Can't locate GameController object");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Boundary")
			|| other.CompareTag ("Asteroid")
			|| other.CompareTag ("Enemy")
			|| other.CompareTag ("EnemyBolt")) {
			return;
		}

		gameScore.AddScore (scoreValue);

		if (selfExplosion != null) {
			Instantiate (selfExplosion, transform.position, transform.rotation);
		}
		if (other.tag == "Player") {
			Instantiate (playerExplosiion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
