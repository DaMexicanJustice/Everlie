using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptReject : MonoBehaviour {

	public enum State
	{
		Up,
		Down,
		Left,
		Right,
	}

	public State state;
	public float magnitudeLimit;

	private int yesCount;
	private int noCount;
	private Vector2 startPos;
	private List<Vector2> recordedPosistions = new List<Vector2>();
	private Vector2 sum;
	private Vector2 average;
	private Vector2 directionVector;

	IEnumerator waitForNextSample(){
		yield return new WaitForSeconds (0.5f);
		average = Vector2.zero;
		for (int i = 0; i < recordedPosistions.Count; i++) {
			average += recordedPosistions [i];
		}

		directionVector = average;

		if (directionVector.magnitude > magnitudeLimit) {
			switch (state) {

			case State.Down:

				if (Mathf.Abs (directionVector.x) > Mathf.Abs (directionVector.y)) {

					yesCount = 0;

					if (directionVector.x > 0) {
						state = State.Right;
					} else {
						state = State.Left;
					}

				} else {

					if (directionVector.y > 0) {
						yesCount++;
						state = State.Up;
					}
				}
				break;

			case State.Up:

				if (Mathf.Abs (directionVector.x) > Mathf.Abs (directionVector.y)) {

					yesCount = 0;

					if (directionVector.x > 0) {
						state = State.Right;
					} else {
						state = State.Left;
					}

				} else {

					if (directionVector.y < 0) {
						yesCount++;
						state = State.Down;
					}
				}

				break;

			case State.Left:

				if (Mathf.Abs (directionVector.y) > Mathf.Abs (directionVector.x)) {

					noCount = 0;

					if (directionVector.y > 0) {
						state = State.Up;
					} else {
						state = State.Down;
					}

				} else {

					if (directionVector.x > 0) {
						noCount++;
						state = State.Right;
					}
				}

				break;

			case State.Right:

				if (Mathf.Abs (directionVector.y) > Mathf.Abs (directionVector.x)) {

					noCount = 0;

					if (directionVector.y > 0) {
						state = State.Up;
					} else {
						state = State.Down;
					}

				} else {

					if (directionVector.x < 0) {
						noCount++;
						state = State.Left;
					}
				}

				break;	
			}
		}
		recordedPosistions.Clear ();
		StartCoroutine (waitForNextSample ());
	}

	void Awake(){
		StartCoroutine (waitForNextSample ());
	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			recordedPosistions.Add (touch.deltaPosition);
		}
	}
}
