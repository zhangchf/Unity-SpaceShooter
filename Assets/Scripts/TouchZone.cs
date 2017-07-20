using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

	public float smoothing = 0.1f;

	private bool touched;
	private int pointerId;
	private Vector2 originalPosition;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private Vector2 movement;

	public void Awake() {
		touched = false;
		direction = Vector2.zero;
		smoothDirection = Vector2.zero;
	}

	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log ("OnPointerDown, pointerId=" + eventData.pointerId + ", position=" + eventData.position);
		if (!touched) {
			touched = true;
			pointerId = eventData.pointerId;
			originalPosition = eventData.position;
			direction = Vector2.zero;
			smoothDirection = Vector2.zero;
		}		
	}

	public void OnPointerUp(PointerEventData eventData) {
		Debug.Log ("OnPointerUp, pointerId=" + eventData.pointerId + ", position=" + eventData.position);
		if (eventData.pointerId == pointerId) {
			touched = false;
			direction = Vector2.zero;
		}		
	}

	public void OnDrag(PointerEventData eventData) {
		Debug.Log ("OnPointerDrag, pointerId=" + eventData.pointerId + ", position=" + eventData.position);
		if (eventData.pointerId == pointerId) {
			Vector2 directionRaw = eventData.position - originalPosition;
			direction = directionRaw.normalized;
			movement = directionRaw;
			Debug.Log ("OnPointerDrag, direction=" + direction);
		}
	}

	public Vector2 GetDirection() {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}

	public Vector2 GetMovement() {
		return movement;
	}

}
