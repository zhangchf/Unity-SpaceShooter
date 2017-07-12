using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnPositionVector;
	public float hazardCountPerWave;
	public float startDelay;
	public float hazardDelay;
	public float wavesDeley;

	public Text gameOverText;
	public Text restartText;

	bool isGameOver = false;
	bool canRestart = false;

	void Start() {
		gameOverText.text = "";
		restartText.text = "";
		StartCoroutine (SpawnWaves ());
	}

	void Update() {
		if (canRestart && Input.GetKeyDown (KeyCode.R)) {
			Restart ();
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startDelay);

		while(true) {
			for (int i = 0; i < hazardCountPerWave; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnPositionVector.x, spawnPositionVector.x), spawnPositionVector.y, spawnPositionVector.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (hazardDelay);
			}
			yield return new WaitForSeconds (wavesDeley);
			if (isGameOver) {
				restartText.text = "Press 'R' to Restart";
				canRestart = true;
				break;
			}
		}
	}

	public void GameOver() {
		isGameOver = true;
		gameOverText.text = "Game Over";
	}

	public void Restart() {
		SceneManager.LoadScene (0);
	}
}
