﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour {
	
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}

}
