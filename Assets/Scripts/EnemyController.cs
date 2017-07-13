using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawnPoint;
	public float fireStartDelay;
	public float fireRepeatRate;


	void Start() {
		InvokeRepeating ("Fire", fireStartDelay, fireRepeatRate);
		
	}

	void Fire() {
		Instantiate (shot, shotSpawnPoint.position, shotSpawnPoint.rotation);
	}

}
