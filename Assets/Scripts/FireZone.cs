using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private bool touched;
	private int pointerId;

	public void OnPointerDown(PointerEventData eventData) {
		if (!touched) {
			touched = true;
			pointerId = eventData.pointerId;
		}		
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (eventData.pointerId == pointerId) {
			touched = false;
		}		
	}

	public bool CanFire() {
		return touched;
	}

}
