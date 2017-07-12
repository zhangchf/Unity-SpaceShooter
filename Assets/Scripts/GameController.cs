using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnPositionVector;
	public float hazardCountPerWave;
	public float startDelay;
	public float hazardDelay;
	public float wavesDeley;


	void Start() {
		StartCoroutine (SpawnWaves ());
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
		}
	}
}
