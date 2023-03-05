using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	[SerializeField] float approachSpeed = 7;

	[SerializeField] Type type = Type.CurveInside;
	[SerializeField] float sharpnessThreshold = 0.5f;



	private float dist;


	public Vector3 position {
		get {
			return transform.position;
		}
	}

	enum Type {
		Start,
		CurveAround,
		CurveInside,
		CheckIn,
		End,
		Finish
	}


	public Vector3 nextPosition (Vector3 currentPos) {
//		print(name + " position: " + currentPos);

		Vector3 newPos = Vector3.MoveTowards(currentPos, position, approachSpeed*Time.deltaTime);
		dist = Vector2.Distance(newPos, transform.position);

		return newPos;
	}

	public bool MoveToNext () {
		bool moveToNext = false;
		if (dist < sharpnessThreshold)
			return true;

		return moveToNext;
	}



}
