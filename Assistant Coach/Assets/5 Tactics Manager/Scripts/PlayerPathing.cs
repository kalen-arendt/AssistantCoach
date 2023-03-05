using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathing : MonoBehaviour {

//	[SerializeField] TBoardPlayer player = null;


	[SerializeField] bool drawGizmos = true;


	State state;
	Color color;

	private int index = 0;
	private Vector3 currentPos;
	private Waypoint targetPos;


	enum State {
		Standby,
		Moving,
		Finished
	}


	void Start () {
		Reset ();
	}


	void nextWaypoint () {
		index = (index+1) % transform.childCount; // increment the index
		if (index == 0)
			state = State.Finished;
		else
			targetPos = transform.GetChild(index).GetComponent<Waypoint> (); // set the new target position
	}


	public Vector3 nextPosition () {
		if (state != State.Finished) {
			currentPos = targetPos.nextPosition(currentPos);
			if (targetPos.MoveToNext())
				nextWaypoint();
		}

		return currentPos;
	}


	public void SetPlayer (TBoardPlayer player) {
//		this.player = player;
		color = player.teamColor;
		color = new Color (color.a, color.g, color.b, 0.6f);
	}

	public void Reset () {
		state = State.Standby;
		index = 0;

		targetPos = transform.GetChild(index).GetComponent<Waypoint> (); // get the startPosition
		currentPos = targetPos.position;
	}

	void OnDrawGizmos () {
		if (!drawGizmos)
			return;

		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, 1);


		if (transform.childCount > 0) {
			Vector3 lastPos = transform.GetChild(0).position;

			foreach (Transform waypoint in transform) {
				Gizmos.color = new Color (0,0,0,0.8f);
				Gizmos.DrawSphere(waypoint.position, 0.25f);
				Gizmos.DrawLine(lastPos, waypoint.position);
				lastPos = waypoint.position;
			}
		}
	}


}
