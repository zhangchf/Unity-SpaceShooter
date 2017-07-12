using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

	public Text scoreText;

	float score = 0f;

	void Start() {
		score = 0f;
		UpdateScore ();
	}

	public void AddScore(float scoreToAdd) {
		score += scoreToAdd;
		UpdateScore ();
	}

	public void UpdateScore() {
		scoreText.text = "Score: " + score;
	}
}
