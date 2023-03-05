
namespace Tactics {
	
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class TacticsPlayer : MonoBehaviour {


		private List<KeyMoment> moments = new List<KeyMoment> ();
		private int targetIndex = 0;



		// Use this for initialization
		void Start () {
			
		}

		void OnEnable () {
			TacticsManager.newKeyMoment += TacticsManager_newKeyMoment;
			TacticsManager.updatePositions += UpdatePosition;
		}

		void OnDisable () {
			TacticsManager.newKeyMoment -= TacticsManager_newKeyMoment;
		}

		void TacticsManager_newKeyMoment (float time) {
//			moments.Add(new KeyMoment(this, time));
			AddKeyMoment(new KeyMoment(this, time));
		}

		void UpdatePosition (float time) {
			var moment = getNextMoment(time);
			float timeTillTarget = moment.time - time;
			float t = Time.deltaTime/timeTillTarget;

			if (timeTillTarget > 0) {
				transform.position = Vector3.Lerp(transform.position, moments[targetIndex].getPosition(), t);
			}
		}


		void AddKeyMoment (KeyMoment keyMoment) {
			int foundIndex;
			for (foundIndex = 0; foundIndex < moments.Count; foundIndex++) {
				if (moments[foundIndex].time > keyMoment.time)
					break;
			}

			moments.Insert(foundIndex, keyMoment);
		}

		private KeyMoment getNextMoment (float time) {
			return moments[targetIndex];
		}

		struct KeyMoment {

			Vector3 position;
			public readonly float time;
			float direction; // only the z rotation matters for 2D

			public KeyMoment (TacticsPlayer player, float time) {
				position = player.transform.position;
				this.time = time;
				direction = player.transform.localRotation.eulerAngles.z;
			}

			public Vector3 getPosition () { return position; }
			public float getDirection () { return direction; }

			//public float getTime () { return Time; }

			//Vector3 position, float time, float xRotation

		}
	}
}
