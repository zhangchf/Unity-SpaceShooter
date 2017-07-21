using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnPositionVector;
	public Vector3 spawnReversePositionVector;
	public float hazardCountPerWave;
	public float startDelay;
	public float hazardDelay;
	public float wavesDeley;

	public GameObject fireZoneLeft;
	public GameObject fireZoneRight;
	public GameObject touchZone;

	public Text gameOverText;
//	public Text restartText;
	public GameObject restartButton;

	bool isGameOver = false;
	bool canRestart = false;

	void Start() {
		gameOverText.text = "";
//		restartText.text = "";
		restartButton.SetActive (false);

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
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];

				bool spawnReverse = Random.value > 0.8;
				Vector3 spawnPos = spawnPositionVector;
				if (spawnReverse) {
					spawnPos = spawnReversePositionVector;
				}
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnPos.x, spawnPos.x), spawnPos.y, spawnPos.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject clone = Instantiate (hazard, spawnPosition, spawnRotation);
				// Reverse the hazard's facing direction and move direction.
				if (spawnReverse) {
					clone.transform.Rotate (new Vector3(0f, 180f, 0f));
				}
				yield return new WaitForSeconds (hazardDelay);
			}
			yield return new WaitForSeconds (wavesDeley);
			if (isGameOver) {
//				restartText.text = "Press 'R' to Restart";
				restartButton.SetActive (true);
				canRestart = true;
				break;
			}
		}
	}

	public void GameOver() {
		isGameOver = true;
		gameOverText.text = "Game Over";
		EnableTouchAndFireZone (false);
	}

	public void Restart() {
		SceneManager.LoadScene (0);
		EnableTouchAndFireZone (true);
	}

	void EnableTouchAndFireZone(bool enable) {
		fireZoneLeft.SetActive (enable);
		fireZoneRight.SetActive (enable);
		touchZone.SetActive (enable);
	}
}
